using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HitManager))]
[RequireComponent(typeof(CurrentState))]
public class StunReceiver : MonoBehaviour, IHitReceiver
{
    private CurrentState currentState;
    public float stunResistance;
    public UnityEvent<float> onStunReceived;


    void Start()
    {
        currentState = GetComponent<CurrentState>();
        if (TryGetComponent(out HitManager hm) && hm.resistance != null)
        {
            stunResistance = hm.resistance.stunResistance;
        }
    }

    public void ReceiveHit(Hit hit)
    {
        Stun(ref hit.stunTime);
    }

    public void Stun(ref float stunTime)
    {
        if (stunTime > 0)
        {
            stunTime -= stunResistance;
            if (stunTime < 0) stunTime = 0;
            StartCoroutine(StunCountDown(stunTime));
            onStunReceived.Invoke(stunTime);
        }
    }

    public void Stun(float stunTime)
    {
        if (stunTime > 0)
        {
            stunTime -= stunResistance;
            if (stunTime < 0) stunTime = 0;
            StartCoroutine(StunCountDown(stunTime));
            onStunReceived.Invoke(stunTime);
        }
    }
    private IEnumerator StunCountDown(float duration)
    {
        currentState.state = State.Stunned;
        yield return new WaitForSeconds(duration);
        currentState.state = State.Targgeting;
    }
}
