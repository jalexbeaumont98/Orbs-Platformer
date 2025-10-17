using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPad : MonoBehaviour
{
    [SerializeField] TeleportController controller;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (controller != null && other.CompareTag("Player"))
        {
            controller.TriggerTeleport(other);
        }
    }
}
