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
        if (other.TryGetComponent(out Living living))
        {
            switch (directionType)
            {
                case DirectionOption.FromCenterOut:
                    living.GetDamaged(new(damage, stunTime, force, (other.transform.position - transform.position).normalized));
                    break;
                case DirectionOption.FromCenterIn:
                    living.GetDamaged(new(damage, stunTime, force, (transform.position - other.transform.position).normalized));
                    break;
                case DirectionOption.Custom:
                    living.GetDamaged(new(damage, stunTime, force, direction.normalized));
                    break;
            }
        }
    }

    private enum DirectionOption
    {
        FromCenterOut,
        FromCenterIn,
        Custom
    } 
}
