using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlls : MonoBehaviour
{
    private InputAction moveAction;

    [SerializeField] private Camera cameraReference;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] EntityActivity currentActivity;

    private Vector3 movementDirection;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        GetMovementDirection();
    }

    void FixedUpdate()
    {
        Move();
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
}
