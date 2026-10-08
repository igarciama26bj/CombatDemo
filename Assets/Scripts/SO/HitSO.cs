using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "HitOS", menuName = "Scriptable Objects/HitOS")]
public class HitOS : ScriptableObject
{
    public DirectionOption directionType;
    public Vector3 direction;
    [SerializedDictionary("Damage Type", "Damage")] public SerializedDictionary<DamageType, int> damages;
    public float force;
    public float stunTime;

    public Dictionary<DamageType, int> GetDamages()
    {
        Dictionary<DamageType, int> d = new();
        foreach (var type in damages.Keys)
            d[type] = damages[type];
        return d;
    }

    public enum DirectionOption
    {
        FromCenterOut,
        FromCenterIn,
        Custom
    }

    public Vector3 GetDirection(Vector3 v1, Vector3 v2)
    {
        return directionType switch
        {
            DirectionOption.FromCenterOut => (v2 - v1).normalized,
            DirectionOption.FromCenterIn => (v1 - v2).normalized,
            DirectionOption.Custom => direction.normalized,
            _ => direction.normalized
        };
    }
}
