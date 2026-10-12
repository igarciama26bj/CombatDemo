using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HitDealer : MonoBehaviour
{
    public HitOS hit;
    public UnityEvent<Hit> onHitDealt;

    private GameObject owner;
    private Collider hitCollider;

    void Start()
    {
        hitCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HitManager hitManager))
        {
            Hit hitFeedback = new(
                owner,
                hit.GetDamages(),
                hit.stunTime,
                hit.GetDirectionVector(transform.position, transform.forward, other.gameObject.transform.position),
                hit.attached,
                hit.force,
                hitCollider
            );
            hitManager.ReceiveHit(hitFeedback);
            onHitDealt.Invoke(hitFeedback);

            if (hitFeedback.attached && owner.TryGetComponent(out ForceReceiver fr))
            {
                fr.Push(hit.force - hitFeedback.force, -hitFeedback.direction);
            }
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}
