using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Collider damageCollider;
    [SerializeField] private int damage;
    [SerializeField] private float force;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Living living))
        {
            living.GetDamaged(new(damage, force, damageCollider));
        }
    }
}
