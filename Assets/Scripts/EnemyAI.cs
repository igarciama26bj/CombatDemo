using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CurrentState))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private CurrentState currentState;

    [SerializeField] GameObject target;
    [SerializeField] float attackDistance;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = attackDistance;
        currentState = GetComponent<CurrentState>();
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
}
