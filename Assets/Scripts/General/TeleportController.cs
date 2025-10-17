using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [Header("Teleport Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Cameras")]
    public CinemachineVirtualCamera camA;
    public CinemachineVirtualCamera camB;

    [Header("Settings")]
    public float cooldownTime = 2f;     // seconds before it can trigger again
    private bool isOnCooldown = false;

    public void TriggerTeleport(Collider2D other)
    {
        if (isOnCooldown) return;
        if (!other.CompareTag("Player")) return;

        Transform player = other.transform;

        // Determine which point is closer
        float distToA = Vector2.Distance(player.position, pointA.position);
        float distToB = Vector2.Distance(player.position, pointB.position);

        // Teleport to the opposite point
        if (distToA < distToB)
        {
            player.position = pointB.position;
            SwapCameras(camA, camB);
        }
        else
        {
            player.position = pointA.position;
            SwapCameras(camB, camA);
        }

        // Start cooldown
        StartCoroutine(TeleportCooldown());
    }

    private void SwapCameras(CinemachineVirtualCamera disableCam, CinemachineVirtualCamera enableCam)
    {
        if (disableCam != null) disableCam.Priority = 0;
        if (enableCam != null) enableCam.Priority = 20;
    }

    private System.Collections.IEnumerator TeleportCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }

}
