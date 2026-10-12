using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "HitOS", menuName = "Scriptable Objects/HitOS")]
public class HitOS : ScriptableObject
{
    [SerializedDictionary("Damage Type", "Damage")] public SerializedDictionary<DamageType, int> damages;
    public float stunTime = 0f;
    public DirectionOption direction = DirectionOption.FromCenterOut;
    [Range(0, 360)] public float arc = 360;
    public bool attached = true;
    public float force = 0f;

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
        FromCenterIn
    }

    public Vector3 GetDirectionVector(Vector3 origin, Vector3 originForward, Vector3 end)
    {
        Vector3 directionVector = end - origin;

        directionVector.y = 0;
        directionVector.Normalize();
        originForward.y = 0;
        originForward.Normalize();
        
        float angle = Vector3.SignedAngle(originForward, directionVector, Vector3.up);
        float halfArc = arc / 2;
        
        if (angle > halfArc)
            directionVector = Quaternion.Euler(0, halfArc, 0) * originForward;
        else if (angle < -halfArc)
            directionVector = Quaternion.Euler(0, -halfArc, 0) * originForward;

        return direction switch
        {
            DirectionOption.FromCenterOut => directionVector,
            DirectionOption.FromCenterIn => -directionVector,
            _ => throw new System.NotImplementedException()
        };
    }
}
