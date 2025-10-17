using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerHP : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<GameObject> hearts;
    void OnEnable()
    {
        PlayerController.OnPlayerHPChange += UpdateHealthUI;
    }

    void OnDisable()
    {
        PlayerController.OnPlayerHPChange -= UpdateHealthUI;
    }


    private void UpdateHealthUI(int hp)
    {
        if (hp <= 0)
        {
            hearts[0].SetActive(false);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }

        if (hp == 1)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }

        if (hp == 2)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(false);
        }

        if (hp == 3)
        {
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(true);
        }
    }

}
