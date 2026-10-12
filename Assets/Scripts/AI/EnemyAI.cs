using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CurrentState))]
[RequireComponent(typeof(HitManager))]
public class EnemyAI : MonoBehaviour
{
    private Rigidbody rigidBody;
    private NavMeshAgent navMeshAgent;
    private CurrentState currentState;
    private HitManager hitManager;

    [SerializeField] GameObject target;
    [SerializeField] float attackDistance;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentState = GetComponent<CurrentState>();
        hitManager = GetComponent<HitManager>();

        navMeshAgent.stoppingDistance = attackDistance;
    }

    void FixedUpdate()
    {
        switch (currentState.state)
        {
            case State.Targgeting:
                Targgeting();
                break;
            case State.Stunned:
                if (navMeshAgent.enabled)
                    navMeshAgent.destination = transform.position;
                break;
        }
    }

    void Targgeting()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.destination = target.transform.position;
    }

    public void OnDead()
    {
        navMeshAgent.enabled = false;
        hitManager.enabled = false;
        rigidBody.freezeRotation = false;
        Destroy(gameObject, 5f);
    }
}
