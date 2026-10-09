using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hit
{
    public GameObject owner;
    public Dictionary<DamageType, int> damages;
    public float stunTime;
    public float pushForce;
    public Vector3 direction;
    public Collider collider;
    public List<HitResult> results;

    public int TotalDamage => damages.Values.Sum();

    public Hit(GameObject owner, Dictionary<DamageType, int> damages, float stunTime, float pushForce, Vector3 direction, Collider collider)
    {
        this.owner = owner;
        this.damages = damages;
        this.stunTime = stunTime;
        this.pushForce = pushForce;
        this.direction = direction;
        this.collider = collider;
        results = new();
    }

    public Hit(Dictionary<DamageType, int> damages, float stunTime, float pushForce, Vector3 direction, Collider collider, List<HitResult> results)
    {
        owner = null;
        this.damages = damages;
        this.stunTime = stunTime;
        this.pushForce = pushForce;
        this.direction = direction;
        this.collider = collider;
        this.results = results;
    }
}