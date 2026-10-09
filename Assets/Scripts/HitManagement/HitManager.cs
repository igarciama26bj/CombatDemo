using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    public readonly List<IHitResistor> hitResistors = new();
    
    private IHitReceiver[] hitReceivers;

    void Start()
    {
        hitReceivers = GetComponents<IHitReceiver>();
    }

    public void ReceiveHit(Hit hit)
    {
        foreach (IHitResistor hitResistor in hitResistors)
            hitResistor.ResistHit(hit);
        foreach (IHitReceiver receiver in hitReceivers)
            receiver.ReciveHit(hit);
    }
}
