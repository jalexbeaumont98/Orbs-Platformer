using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMan : MonoBehaviour
{

    public GameObject hearts;
    public GameObject deathMenu;

    public static MenuMan Instance { get; private set; }


    public static event System.Action OnNextLevel;
    
    void Awake()
    {
        // Singleton pattern (ensures one persistent GameState)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
    }
    
   

    public void SetDeathMenu()
    {
        hearts.SetActive(false);

        deathMenu.SetActive(true);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        
    }
    
    public void GoToNextLevel(string level)
    {
        OnNextLevel?.Invoke();
        SceneManager.LoadScene(level);
    }
}
