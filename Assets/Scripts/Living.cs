using System.Collections;
using UnityEngine;

public class Living : MonoBehaviour
{
    [SerializeField] private CurrentState currentState;
    [SerializeField] private int life;
    [SerializeField, Range(0,1)] private float stunResistance;
    [SerializeField, Range(0,1)] private float pushResistance;

    public void GetDamaged(Hit hitData)
    {
        life -= hitData.damage;

        if (hitData.stunTime >= 0)
        {
            StartCoroutine(Stun(hitData.stunTime * (1 - stunResistance)));
        }

        if (hitData.pushForce >= 0 && TryGetComponent(out Rigidbody rb)) {
            rb.AddForce(hitData.pushForce * (1 - pushResistance) * hitData.direction, ForceMode.Impulse);
        }

        if (life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject, 0.5f);
    }

    IEnumerator Stun(float duration)
    {
        currentState.state = State.Stunned;
        yield return new WaitForSeconds(duration);
        currentState.state = State.Targgeting;
    }
}
