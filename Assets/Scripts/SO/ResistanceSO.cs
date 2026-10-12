using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageResistancesSO", menuName = "Scriptable Objects/DamageResistancesSO")]
public class ResistanceSO : ScriptableObject
{
    public int HP;
    [SerializedDictionary("Damage Type", "Resistance")] public SerializedDictionary<DamageType, int> damageResistances;
    public float stunResistance;
    public float forceResistance;
}
