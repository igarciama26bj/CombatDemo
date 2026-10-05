using System.Collections;
using UnityEngine;

public class Living : MonoBehaviour, IHitGetter
{
    [SerializeField] private int HP;
    [SerializeField] private int damageResistance;

    public void GetHit(Hit hit)
    {
        hit.damage -= damageResistance;
        if (hit.damage == 0) hit.damage = 0;
        HP -= hit.damage;

        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject, 0.5f);
    }
}
