using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public GameObject info;
    public GameObject menu;

    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ToggleInfo()
    {
        info.SetActive(!info.activeSelf);
        menu.SetActive(!menu.activeSelf);

    }
    public void Quit()
    {
        Application.Quit();
    }
}
