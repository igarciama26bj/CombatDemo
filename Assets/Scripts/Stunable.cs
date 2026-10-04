using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CurrentState))]
public class Stunable : MonoBehaviour
{
    private CurrentState cs;

    void Start()
    {
        cs = GetComponent<CurrentState>();
    }

    public IEnumerator Stun(float time)
    {
        cs.state = State.Stunned;
        yield return new WaitForSeconds(time);
        cs.state = State.Targgeting;
    }
}
