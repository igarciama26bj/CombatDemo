using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private DirectionOption directionType;
    [SerializeField] private Vector3 direction;
    [SerializeField] private int damage;
    [SerializeField] private float force;
    [SerializeField] private float stunTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HitManager hitManager))
        {
            hitManager.GetHit(new(damage, stunTime, force, GetDirection(other), GetComponent<Collider>()));
        }
    }

    private Vector3 GetDirection(Collider other)
    {
        return directionType switch
        {
            DirectionOption.FromCenterOut => (other.transform.position - transform.position).normalized,
            DirectionOption.FromCenterIn => (transform.position - other.transform.position).normalized,
            DirectionOption.Custom => direction.normalized,
            _ => direction.normalized
        };
    }

    private enum DirectionOption
    {
        FromCenterOut,
        FromCenterIn,
        Custom
    } 
}
