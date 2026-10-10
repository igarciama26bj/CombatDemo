using UnityEngine;

public class HitDealer : MonoBehaviour
{
    public HitOS hit;
    private GameObject owner;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HitManager hitManager))
        {
            Hit hitFeedback = new(
                owner,
                hit.GetDamages(),
                hit.stunTime,
                hit.force,
                hit.GetDirectionVector(transform.position, transform.forward, other.gameObject.transform.position),
                GetComponent<Collider>()
            );
            hitManager.ReceiveHit(hitFeedback);
            hitFeedback.owner = null;

            if (owner.TryGetComponent(out ForceReceiver fr))
            {
                fr.Push((hit.force - hitFeedback.pushForce) * -transform.right);
            }
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}
