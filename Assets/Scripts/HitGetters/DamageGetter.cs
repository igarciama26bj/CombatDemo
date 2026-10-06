using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageGetter : MonoBehaviour, IHitGetter
{
    [SerializeField] private int HP;
    [SerializeField] private List<SerializableDamageByType> damageResistances;

    public void GetHit(Hit hit)
    {
        Dictionary<DamageType, int> resistances = damageResistances.ToDictionary(d => d.damageType, d => d.value);
        
        foreach (var damageType in resistances.Keys)
        {
            if (resistances.TryGetValue(damageType, out int resistance))
            {
                hit.damages[damageType] -= resistance;
                if (hit.damages[damageType] < 0)
                    hit.damages[damageType] = 0;
            }
            HP -= hit.damages[damageType];
        }

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
