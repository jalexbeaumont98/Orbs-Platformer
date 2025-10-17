using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Platform")) return;

        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.transform.GetComponent<PlayerController>();

            if (player)
            {
                player.TakeDamage(damage);
            }
        }

        if (hitEffect)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
