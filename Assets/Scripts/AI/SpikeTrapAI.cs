using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CurrentState))]
public class SpikeTrapAI : MonoBehaviour
{
    private CurrentState currentState;

    [SerializeField] private GameObject spikeTrapAttack;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activationDuration;

    void Start()
    {
        currentState = GetComponent<CurrentState>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentState.state == State.Idle)
            StartCoroutine(ActivateTrap());
    }

    private IEnumerator ActivateTrap()
    {
        currentState.state = State.Attacking;
        yield return new WaitForSeconds(activationDelay);
        GameObject attack = Instantiate(spikeTrapAttack, gameObject.transform.parent);
        attack.GetComponent<HitDealer>().SetOwner(gameObject.transform.parent.gameObject);
        attack.transform.Translate(0, 1f, 0);
        yield return new WaitForSeconds(activationDuration);
        Destroy(attack);
        currentState.state = State.Idle;
    }
}
