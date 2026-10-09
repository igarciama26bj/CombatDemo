using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DamageReceiver : MonoBehaviour, IHitReceiver
{
    [SerializeField] private int HP;
    [SerializeField] private DamageResistancesSO damageResistances;

    public void GetHit(Hit hit)
    {
        DamageType[] damages = hit.damages.Keys.ToArray();
        foreach (var damageType in damages)
        {
            if (damageResistances.resistances.TryGetValue(damageType, out int resistance))
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
        if (TryGetComponent(out NavMeshAgent nma))
            nma.enabled = false;
        Destroy(gameObject, 1f);
    }
}
