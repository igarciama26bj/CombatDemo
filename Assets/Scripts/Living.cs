using UnityEngine;

public class Living : MonoBehaviour
{
    [SerializeField] private int life;

    public void GetDamaged(DamageData damageData)
    {
        life -= damageData.damage;

        if (TryGetComponent(out Stunable s))
        {
            StartCoroutine(s.Stun(damageData.stunTime));
        }

        if (TryGetComponent(out Rigidbody rb)) {
            Vector3 forceVector = (transform.position - damageData.collider.transform.position).normalized;
            rb.AddForce(forceVector * damageData.force, ForceMode.Impulse);
        }

        if (life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject, 0.5f);
    }
}
