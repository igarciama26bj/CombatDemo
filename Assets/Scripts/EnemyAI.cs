using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent nma;

    [SerializeField] GameObject target;
    [SerializeField] float attackDistance;
    [SerializeField] EntityActivity currentActivity;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        nma.stoppingDistance = attackDistance;
    }

    void FixedUpdate()
    {
        switch (currentActivity)
        {
            case EntityActivity.Targgeting:
                Targgeting();
                break;
            case EntityActivity.Stunned:
                break;
        }
    }

    void Targgeting()
    {
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) > attackDistance)
        {
            nma.destination = target.transform.position;
        }
    }

    void Stunned()
    {
        if (TryGetComponent(out NavMeshAgent nma))
        {
            nma.enabled = false;
            Invoke(nameof(UnStun), 2);
        }
    }

    void UnStun()
    {
        if (TryGetComponent(out NavMeshAgent nma))
        {
            nma.enabled = true;
            currentActivity = EntityActivity.Targgeting;
        }
    }
}
