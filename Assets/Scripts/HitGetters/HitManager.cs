using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitManager : MonoBehaviour
{
    public UnityEvent<Hit> onHitTaken;
    public readonly List<IHitResistor> hitResistors = new();

    public Hit GetHit(Hit hit)
    {
        foreach (IHitResistor hitResistor in hitResistors)
            hitResistor.ResistHit(hit);
        onHitTaken.Invoke(hit);
        return hit;
    }
}
