using UnityEngine;

public struct DamageData
{
    public int damage;
    public float force;
    public float stunTime;
    public Collider collider;

    public DamageData(int damage, float force, float stunTime, Collider collider)
    {
        this.damage = damage;
        this.force = force;
        this.stunTime = stunTime;
        this.collider = collider;
    }
}