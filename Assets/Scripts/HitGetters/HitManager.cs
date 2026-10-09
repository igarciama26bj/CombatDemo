using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public readonly List<IHitResistor> hitResistors = new();

    public void ReceiveHit(Hit hit)
    {
        foreach (IHitResistor hitResistor in hitResistors)
            hitResistor.ResistHit(hit);
        foreach (var receiver in GetComponents<IHitReceiver>())
            receiver.GetHit(hit);
    }
}
