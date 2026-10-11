using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CurrentState))]
public class PlayerControlls : MonoBehaviour
{
    private CurrentState currentState;
    private InputAction moveAction;
    private InputAction attackAction;

    [SerializeField] private Camera cameraReference;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject playerAttack;

    private Vector3 movementDirection;

    void Start()
    {
        currentState = GetComponent<CurrentState>();
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        GetMovementDirection();
        if (currentState.state == State.Idle && attackAction.ReadValue<float>() != 0)
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        switch(currentState.state)
        {
            case State.Idle:
                Move();
                break;
            case State.Attacking:
                break;
        }
    }

    private void GetMovementDirection()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 cameraForward = cameraReference.transform.forward;
        Vector3 cameraRight = cameraReference.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        movementDirection = cameraForward * input.y + cameraRight * input.x;
    }

    private void Move()
    {
        if (movementDirection.magnitude != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movementDirection), rotationSpeed * Time.fixedDeltaTime);
            transform.Translate(movementSpeed * Time.fixedDeltaTime * movementDirection, Space.World);
        }
    }

    private IEnumerator Attack()
    {
        currentState.state = State.Attacking;
        GameObject attack = Instantiate(playerAttack, gameObject.transform);
        attack.GetComponent<HitDealer>().SetOwner(gameObject);
        attack.transform.Translate(0, 0, 1.2f);
        yield return new WaitForSeconds(0.4f);
        Destroy(attack);
        currentState.state = State.Idle;
    }
}
