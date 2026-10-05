using System.Collections.Generic;
using UnityEngine;

public struct Hit
{
    public int damage;
    public float stunTime;
    public float pushForce;
    public Vector3 direction;
    public Collider collider;
    public List<HitResult> results;

    public Hit(int damage, float stunTime, float pushForce, Vector3 direction, Collider collider)
    {
        this.damage = damage;
        this.stunTime = stunTime;
        this.pushForce = pushForce;
        this.direction = direction;
        this.collider = collider;
        results = new();
    }

    public Hit(int damage, float stunTime, float pushForce, Vector3 direction, Collider collider, List<HitResult> results)
    {
        this.damage = damage;
        this.stunTime = stunTime;
        this.pushForce = pushForce;
        this.direction = direction;
        this.collider = collider;
        this.results = results;
    }
}