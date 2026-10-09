using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForceReceiver : MonoBehaviour, IHitReceiver
{
    private Rigidbody rigidBody;

    [SerializeField] private float pushResistance;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void GetHit(Hit hit)
    {
        hit.pushForce -= pushResistance;
        if (hit.pushForce < 0)
            hit.pushForce = 0;
        Push(hit.pushForce, hit.direction);
        print($"new force {hit.pushForce}");
    }

    private void Push(float pushForce, Vector3 direction)
    {
        rigidBody.AddForce(pushForce * direction, ForceMode.Impulse);
    }
}
