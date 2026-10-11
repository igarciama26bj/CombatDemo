using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HitManager))]
[RequireComponent(typeof(Rigidbody))]
public class ForceReceiver : MonoBehaviour, IHitReceiver
{
    private Rigidbody rigidBody;

    [SerializeField] private float pushResistance;
    public UnityEvent<Hit> onForceReceived;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void ReceiveHit(Hit hit)
    {
        hit.pushForce -= pushResistance;
        if (hit.pushForce < 0)
            hit.pushForce = 0;
        Push(hit.pushForce * hit.direction);
        onForceReceived.Invoke(hit);
    }

    public void Push(Vector3 push)
    {
        rigidBody.AddForce(push, ForceMode.Impulse);
    }
}
