using UnityEngine;

public class HitDealer : MonoBehaviour
{
    public HitOS hit;
    public GameObject owner;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HitManager hitManager))
        {
            Hit hitFeedback = new(
                hit.GetDamages(),
                hit.stunTime,
                hit.force,
                hit.GetDirection(transform.position, other.transform.position),
                GetComponent<Collider>()
            );
            hitManager.ReceiverHit(hitFeedback);

            if (owner.TryGetComponent(out Rigidbody r))
            {
                r.AddForce((hit.force - hitFeedback.pushForce) * -transform.forward);
            }
        }
    }
}
