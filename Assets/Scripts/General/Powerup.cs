using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private SphereData sphere;

    [Header("Hover Settings")]
    public float amplitude = 0.25f;   // how far it moves up/down
    public float frequency = 1f;      // how fast it moves
    public float noiseStrength = 0.1f; // random variation in speed
    public float noiseFrequency = 0.5f; // how often the noise changes
    public Transform floater;
    private Animator anim;

    public bool used;

    private Vector3 startPos;
    private float noiseOffset;
    private float timeOffset;

    void Start()
    {
        anim = GetComponent<Animator>();

        startPos = floater.transform.position;
        timeOffset = Random.Range(0f, 100f); // so multiple objects don't move identically
        noiseOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (used) return;

        // Create a smooth base sine wave motion
        float time = Time.time + timeOffset;
        float baseWave = Mathf.Sin(time * frequency) * amplitude;

        // Add a bit of smooth Perlin noise for slight inconsistency
        float noise = (Mathf.PerlinNoise(noiseOffset, time * noiseFrequency) - 0.5f) * 2f * noiseStrength;

        // Combine them for a natural float motion
        float offsetY = baseWave + noise;

        // Apply vertical offset (up/down float)
        floater.transform.position = startPos + new Vector3(0f, offsetY, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (used) return;

        if (collision.CompareTag("Player"))
        {

            PlayerSphereManager sphereMan = FindObjectOfType<PlayerSphereManager>();
            if (sphereMan)
            {
                sphereMan.AddOrbiter(sphere);
            }

            used = true;
            Destroy(floater.gameObject);

            anim.SetBool("used", used);

        }
    }
}
