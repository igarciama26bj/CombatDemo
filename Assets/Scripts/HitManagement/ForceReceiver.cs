using UnityEngine;

[RequireComponent(typeof(HitManager))]
[RequireComponent(typeof(Rigidbody))]
public class ForceReceiver : MonoBehaviour, IHitReceiver
{
    private Rigidbody rigidBody;

    [SerializeField] private float pushResistance;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void ReciveHit(Hit hit)
    {
        hit.pushForce -= pushResistance;
        if (hit.pushForce < 0)
            hit.pushForce = 0;
        Push(hit.pushForce * hit.direction);
    }

    public void Push(Vector3 push)
    {
        rigidBody.AddForce(push, ForceMode.Impulse);
    }
}
