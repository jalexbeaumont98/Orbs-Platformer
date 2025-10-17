using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : PlayerProjectile
{
    [Header("Targeting")]
    public string targetTag = "Enemy";   // Tag to search for
    public float maxTargetRadius = 10f;  // How far to look for targets
    public float turnSpeed = 5f;         // How quickly the projectile can steer

    [Header("Movement")]
    private Transform target;

    private bool initialized = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Call this when you spawn the projectile
    public override void Initialize(ProjectileData data)
    {
        base.Initialize(data);
        // Find nearest target
        target = FindNearestTarget();

        // Apply initial velocity (either toward target or straight)
        Vector2 direction = rb.velocity.normalized;

        initialized = true;
    }

    void FixedUpdate()
    {
        if (!initialized) return;

        if (target)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;

            // Gradually rotate current velocity toward target
            Vector2 newVelocity = Vector2.Lerp(rb.velocity.normalized, direction, turnSpeed * Time.fixedDeltaTime);
            rb.velocity = newVelocity.normalized * speed;

            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
        // else: no target, just keep moving straight
    }

    private Transform FindNearestTarget()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag(targetTag);
        Transform nearest = null;
        float shortestDist = Mathf.Infinity;

        foreach (GameObject c in candidates)
        {
            float dist = Vector2.Distance(transform.position, c.transform.position);
            if (dist < shortestDist && dist <= maxTargetRadius)
            {
                shortestDist = dist;
                nearest = c.transform;
            }
        }

        return nearest;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Platform")) return;

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
        }

        if (hitEffect)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
