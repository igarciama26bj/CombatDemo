using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pushable : MonoBehaviour, IHitGetter
{
    private Rigidbody rigidBody;

    [SerializeField] private float pushResistance;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void GetHit(Hit hit)
    {
        if (hit.pushForce > 0)
        {
            hit.pushForce -= pushResistance;
            if (hit.pushForce < 0) hit.pushForce = 0;
            Push(hit.pushForce, hit.direction);
        }
    }

    private void Push(float pushForce, Vector3 direction)
    {
        rigidBody.AddForce(pushForce * direction, ForceMode.Impulse);
    }
}
