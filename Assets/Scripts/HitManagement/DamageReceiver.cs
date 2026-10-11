using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(HitManager))]
public class DamageReceiver : MonoBehaviour, IHitReceiver
{
    [SerializeField] private int HP;
    [SerializeField] private DamageResistancesSO damageResistances;
    public UnityEvent<Hit> onDamageReceived;
    public UnityEvent onDead;

    public void ReceiveHit(Hit hit)
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
        onDamageReceived.Invoke(hit);

        if (HP <= 0)
        {
            hit.results.Add(HitResult.Died);
            onDead.Invoke();
        }
    }
}
