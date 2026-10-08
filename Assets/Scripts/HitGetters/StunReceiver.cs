using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CurrentState))]
public class StunReceiver : MonoBehaviour, IHitGetter
{
    private CurrentState currentState;
    [SerializeField] private float stunResistance;

    void Start()
    {
        currentState = GetComponent<CurrentState>();
    }

    public void GetHit(Hit hit)
    {
        if (hit.stunTime > 0)
        {
            hit.stunTime -= stunResistance;
            if (hit.stunTime < 0) hit.stunTime = 0;
            StartCoroutine(Stun(hit.stunTime));
        }
    }

    IEnumerator Stun(float duration)
    {
        currentState.state = State.Stunned;
        yield return new WaitForSeconds(duration);
        currentState.state = State.Targgeting;
    }
}
