using UnityEngine;

public struct Hit
{
    public int damage;
    public float stunTime;
    public float pushForce;
    public Vector3 direction;

    public Hit(int damage, float stunTime, float pushForce, Vector3 direction)
    {
        this.damage = damage;
        this.stunTime = stunTime;
        this.pushForce = pushForce;
        this.direction = direction;
    }
}