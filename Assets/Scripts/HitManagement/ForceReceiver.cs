using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HitManager))]
[RequireComponent(typeof(Rigidbody))]
public class ForceReceiver : MonoBehaviour, IHitReceiver
{
    private Rigidbody rigidBody;

    public float forceResistance;
    public UnityEvent<float, Vector3> onForceReceived;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (TryGetComponent(out HitManager hm) && hm.resistance != null)
        {
            forceResistance = hm.resistance.forceResistance;
        }
    }

    public void ReceiveHit(Hit hit)
    {
        Push(ref hit.force, hit.direction);
    }

    public void Push(ref float force, Vector3 direction)
    {
        if (Math.Abs(force) > 0)
        {
            force -= forceResistance;
            if (force < 0)
                force = 0;
            rigidBody.AddForce(force * direction, ForceMode.Impulse);
            onForceReceived.Invoke(force, direction);
        }
    }

    public void Push(float force, Vector3 direction)
    {
        if (Math.Abs(force) > 0)
        {
            force -= forceResistance;
            if (force < 0)
                force = 0;
            rigidBody.AddForce(force * direction, ForceMode.Impulse);
            onForceReceived.Invoke(force, direction);
        }
    }
}
