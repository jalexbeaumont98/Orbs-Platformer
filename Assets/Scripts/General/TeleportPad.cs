using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPad : MonoBehaviour
{
    [SerializeField] TeleportController controller;

    [SerializeField] private bool isLevelTP;
    [SerializeField] private string levelString;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (controller != null && other.CompareTag("Player"))
        {

            if (!isLevelTP)
                controller.TriggerTeleport(other);


        }

        else if (isLevelTP)
        {
            MenuMan.Instance.GoToNextLevel(levelString);
        }
    }
}
