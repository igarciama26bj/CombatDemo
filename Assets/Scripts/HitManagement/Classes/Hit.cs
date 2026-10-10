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

    public Hit(
        GameObject owner = null,
        Dictionary<DamageType, int> damages = null,
        float stunTime = 0,
        float pushForce = 0,
        Vector3 direction = new(), 
        Collider collider = null,
        List<HitResult> results = null
    ) {
        this.owner = owner;
        this.damages = (damages is null) ? new() : damages;
        this.stunTime = stunTime;
        this.pushForce = pushForce;
        this.direction = direction;
        this.collider = collider;
        this.results = (results is null) ? new() : results;
    }
}