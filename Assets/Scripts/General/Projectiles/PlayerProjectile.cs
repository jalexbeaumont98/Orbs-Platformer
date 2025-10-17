using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
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
