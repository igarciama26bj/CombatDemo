using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private DirectionOption directionType;
    [SerializeField] private Vector3 direction;
    [SerializedDictionary("Damage Type", "Damage")] public SerializedDictionary<DamageType, int> damages;
    [SerializeField] private float force;
    [SerializeField] private float stunTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HitManager hitManager))
        {
            Hit hit = hitManager.GetHit(new(
                CloneDamages(),
                stunTime,
                force,
                GetDirection(other),
                GetComponent<Collider>()
            ));
        }
    }

    private Dictionary<DamageType, int> CloneDamages()
    {
        Dictionary<DamageType, int> d = new();
        foreach (var type in damages.Keys)
            d[type] = damages[type];
        return d;
    }

    private Vector3 GetDirection(Collider other)
    {
        return directionType switch
        {
            DirectionOption.FromCenterOut => (other.transform.position - transform.position).normalized,
            DirectionOption.FromCenterIn => (transform.position - other.transform.position).normalized,
            DirectionOption.Custom => direction.normalized,
            _ => direction.normalized
        };
    }

    private enum DirectionOption
    {
        FromCenterOut,
        FromCenterIn,
        Custom
    }
}
