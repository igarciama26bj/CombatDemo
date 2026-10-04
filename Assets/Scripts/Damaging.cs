using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private Collider damageCollider;
    [SerializeField] private int damage;
    [SerializeField] private float force;
    [SerializeField] private float stunTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Living living))
        {
            living.GetDamaged(new(damage, force, stunTime, damageCollider));
        }
    }
}
