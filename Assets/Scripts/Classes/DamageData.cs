using UnityEngine;

public struct DamageData
{
    public int damage;
    public float force;
    public Collider collider;

    public DamageData(int damage, float force, Collider collider)
    {
        this.damage = damage;
        this.force = force;
        this.collider = collider;
    }
}