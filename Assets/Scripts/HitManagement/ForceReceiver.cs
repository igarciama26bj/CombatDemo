using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(HitManager))]
[RequireComponent(typeof(Rigidbody))]
public class ForceReceiver : MonoBehaviour, IHitReceiver
{
    private Rigidbody rigidBody;
    private NavMeshAgent navMeshAgent;

    public float forceResistance;
    public UnityEvent<float, Vector3> onForceReceived;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void ReceiveResistance(ResistanceSO resistance)
    {
        forceResistance = resistance.forceResistance;
    }

    public void ReceiveHit(Hit hit)
    {
        Push(ref hit.force, hit.direction);
    }

    public void Push(float force, Vector3 direction)
    {
        Push(ref force, direction);
    }

    public void Push(ref float force, Vector3 direction)
    {
        if (Math.Abs(force) > 0)
        {
            force -= forceResistance;
            if (force < 0)
                force = 0;
            rigidBody.AddForce(force * direction, ForceMode.Impulse);
            StartCoroutine(WaitUntilStops());
            onForceReceived.Invoke(force, direction);
        }
    }
    
    private IEnumerator WaitUntilStops()
    {
        if (navMeshAgent != null && navMeshAgent.enabled)
        {
            navMeshAgent.enabled = false;
            yield return new WaitUntil(() => rigidBody.linearVelocity.magnitude < 0.1f);
            navMeshAgent.enabled = true;
        }
    }
}
