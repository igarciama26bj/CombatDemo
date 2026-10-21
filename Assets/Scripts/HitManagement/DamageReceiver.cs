using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HitManager))]
public class DamageReceiver : MonoBehaviour, IHitReceiver
{
    public int HP;
    [SerializedDictionary("Damage Type", "Resistance")] public SerializedDictionary<DamageType, int> damageResistances;
    public UnityEvent<Dictionary<DamageType, int>> onDamageReceived;
    public UnityEvent onDead;

    public void ReceiveResistances(ResistancesSO resistances)
    {
        HP = resistances.HP;
        damageResistances = resistances.damageResistances;
        if (HP <= 0) onDead.Invoke();
    }

    public void ReceiveHit(Hit hit)
    {
        Damage(hit.damages, hit.results);
    }

    public void Damage(Dictionary<DamageType, int> damages, List<HitResult> results = null)
    {
        if (damages != null && damages.Values.Sum() > 0)
        {
            DamageType[] damageTypes = damages.Keys.ToArray();
            foreach (var damageType in damageTypes)
            {
                if (damageResistances.TryGetValue(damageType, out int resistance))
                {
                    damages[damageType] -= resistance;
                    if (damages[damageType] < 0)
                        damages[damageType] = 0;
                }
                HP -= damages[damageType];
            }
            onDamageReceived.Invoke(damages);

            if (HP <= 0)
            {
                results.Add(HitResult.Died);
                onDead.Invoke();
            }
        }
    }
}
