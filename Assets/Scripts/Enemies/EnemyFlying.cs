using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    [Header("References")]
    public PolygonCollider2D movementBounds;   // assign in Inspector
    public Transform player;                   // assign at runtime or in Inspector
    public GameObject projectilePrefab;
    public ProjectileData projData;
    public Transform firePoint;                // optional: where the projectile spawns

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float changeTargetInterval = 2f;

    [Header("Attack Settings")]
    public float detectionRange = 6f;
    public float fireCooldown = 1.5f;
    public float projectileSpeed = 10f;

    private Rigidbody2D rb;
    private Vector2 currentTarget;
    private float fireTimer;
    private Bounds bounds;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();

        if (movementBounds)
            bounds = movementBounds.bounds;

        player = FindObjectOfType<PlayerController>().transform;

        PickNewTarget();
        StartCoroutine(RandomMovementRoutine());
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (player)
        {
            float dist = Vector2.Distance(transform.position, player.position);

            if (dist <= detectionRange && fireTimer <= 0f)
            {
                FireAtPlayer();
                fireTimer = fireCooldown;
            }
        }
    }

    void FixedUpdate()
    {
        // Move toward target
        Vector2 dir = (currentTarget - (Vector2)transform.position).normalized;
        rb.velocity = dir * moveSpeed;

  

        // If close enough to target, pick a new one
        if (Vector2.Distance(transform.position, currentTarget) < 0.5f)
        {
            PickNewTarget();
        }
    }

    private void PickNewTarget()
    {
        if (movementBounds == null)
        {
            Debug.LogWarning("Enemy has no movementBounds assigned!");
            return;
        }

        // Get a random point inside the polygon collider
        Vector2 randomPoint = Vector2.zero;
        int tries = 0;
        do
        {
            randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
            tries++;
        }
        while (!movementBounds.OverlapPoint(randomPoint) && tries < 10);

        currentTarget = randomPoint;
    }

    private IEnumerator RandomMovementRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeTargetInterval);
            PickNewTarget();
        }
    }

    private void FireAtPlayer()
    {
        if (projectilePrefab == null || player == null) return;

        // Direction to player
        Vector2 dir = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Spawn projectile
        Vector3 spawnPos = firePoint ? firePoint.position : transform.position;
        Projectile proj = Instantiate(projectilePrefab, spawnPos, Quaternion.Euler(0, 0, angle)).GetComponent<Projectile>();

        proj.Initialize(projData);
        proj.Launch();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
