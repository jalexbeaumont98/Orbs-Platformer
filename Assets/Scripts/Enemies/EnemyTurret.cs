using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [Header("References")]
    public Transform barrel;            // child transform that rotates to aim
    public Transform firePoint;
    public GameObject projectilePrefab;    // where projectiles spawn
    public ProjectileData projData;
    public Transform player;
    public LaserRenderer laser;

    [Header("Settings")]
    public float rotationSpeed = 5f;
    public float fireRate = 2f;         // seconds between shots
    public float detectionRange = 10f;
    public LayerMask obstacleMask;      // layers that block line of sight

    private float fireTimer;

    protected override void Start()
    {
        base.Start();
        
        if (!player)
            player = FindAnyObjectByType<PlayerController>().transform;
    }

    void Update()
    {
        if (!player) return;

        // Direction to player
        Vector2 dir = player.position - barrel.position;
        float distance = dir.magnitude;

        if (distance <= detectionRange)
        {
            // Rotate barrel toward player smoothly
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);
            barrel.rotation = Quaternion.Lerp(barrel.rotation, targetRot, rotationSpeed * Time.deltaTime);

            // Raycast to check line of sight
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir.normalized, detectionRange, obstacleMask);

            if (hit.collider)
                laser.DrawLaser(firePoint.position, hit.point);

            
            if (hit.collider != null && hit.collider.transform == player)
            {
                // Timer for firing
                fireTimer -= Time.deltaTime;
                if (fireTimer <= 0f)
                {
                    Fire();
                    fireTimer = fireRate;
                }
            }
        }
    }

    void Fire()
    {
        Projectile proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation).GetComponent<Projectile>();
        proj.Initialize(projData);
        proj.Launch();

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
