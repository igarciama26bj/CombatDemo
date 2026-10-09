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
                hit.GetDirection(transform.position, other.transform.position),
                GetComponent<Collider>()
            );
            hitManager.ReceiveHit(hitFeedback);

            if (owner.TryGetComponent(out Rigidbody r))
            {
                print($"Push back {hit.force}!");
                r.AddForce(5 * -transform.right, ForceMode.Impulse);
            }
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }
}
