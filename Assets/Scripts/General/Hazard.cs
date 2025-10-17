using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.transform.GetComponent<PlayerController>();

            if (player)
            {
                player.TakeDamage(3);
            }
        }
    }
}
