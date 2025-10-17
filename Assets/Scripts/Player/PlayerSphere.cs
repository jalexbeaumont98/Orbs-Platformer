using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSphere : MonoBehaviour
{
    public SphereData data;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = data.mainSprite;
    }

    public void FireProjectile()
    {
        if (data == null || data.projectileData == null) return;

        // Get world position of the mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        // Compute direction from fire point to mouse
        Vector2 dir = (mouseWorldPos - firePoint.position).normalized;

        // Calculate rotation angle (for a 2D top-down or side view)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Instantiate and rotate projectile
        GameObject projInstance = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

        // Initialize it
        Projectile proj = projInstance.GetComponent<Projectile>();
        proj.Initialize(data.projectileData);

        // Launch it in the direction it’s facing
        proj.Launch();
    }
}
