using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HitManager))]
[RequireComponent(typeof(CurrentState))]
public class StunReceiver : MonoBehaviour, IHitReceiver
{
    private CurrentState currentState;
    [SerializeField] private float stunResistance;
    public UnityEvent<Hit> onStunReceived;


    void Start()
    {
        currentState = GetComponent<CurrentState>();
    }

    public void ReceiveHit(Hit hit)
    {
        if (hit.stunTime > 0)
        {
            hit.stunTime -= stunResistance;
            if (hit.stunTime < 0) hit.stunTime = 0;
            StartCoroutine(Stun(hit.stunTime));
            onStunReceived.Invoke(hit);
        }
    }

    public IEnumerator Stun(float duration)
    {
        currentState.state = State.Stunned;
        yield return new WaitForSeconds(duration);
        currentState.state = State.Targgeting;
    }
}
