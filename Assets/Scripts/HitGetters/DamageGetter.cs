using AYellowpaper.SerializedCollections;
using UnityEngine;

public class DamageGetter : MonoBehaviour, IHitGetter
{
    [SerializeField] private int HP;
    [SerializedDictionary("Damage Type", "Resistance")] public SerializedDictionary<DamageType, int> resistances;

    public void GetHit(Hit hit)
    {
        foreach (var damageType in hit.damages.Keys)
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
