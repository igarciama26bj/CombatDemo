using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageResistancesSO", menuName = "Scriptable Objects/DamageResistancesSO")]
public class DamageResistancesSO : ScriptableObject
{
    [SerializedDictionary("Damage Type", "Resistance")] public SerializedDictionary<DamageType, int> resistances;
}
