using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    [Header("Global Player Data")]
    public int playerHP = 3;

    [Header("Global Object References")]
    public List<SphereData> trackedObjects = new List<SphereData>();
    public PlayerSphereManager sphereMan;
    PlayerController playCon;

    void Awake()
    {
        // Singleton pattern (ensures one persistent GameState)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        

    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Level loaded: {scene.name}");
        InitLevel();
    }

    public void InitLevel()
    {
        // Called every time a new scene loads
        Debug.Log("Initializing level...");

        sphereMan = FindObjectOfType<PlayerSphereManager>();
        playCon = FindObjectOfType<PlayerController>();

        if (sphereMan)
            RespawnOrbs();

        if (playCon)
            playCon.SetHP(playerHP);

        // Example: clean up invalid tracked objects
        trackedObjects.RemoveAll(obj => obj == null);

        // You could respawn persistent helpers, reset cameras, etc.
        // For example:
        // ResetPlayerHP();
    }

    // Optional helpers
    public void RegisterObject(SphereData sphere)
    {
        trackedObjects ??= new List<SphereData>();
        trackedObjects.Add(sphere);
    }

    public void UnregisterObject(SphereData sphere)
    {
        trackedObjects ??= new List<SphereData>();
        trackedObjects.Remove(sphere);
    }

    public void ChangeHP(int amount)
    {
        playerHP += amount;
        Debug.Log($"Player HP = {playerHP}");
    }

    private void RespawnOrbs()
    {
        trackedObjects ??= new List<SphereData>(); 
        
        foreach(SphereData s in trackedObjects)
        {
            sphereMan.AddOrbiter(s);
        }
    }
}
