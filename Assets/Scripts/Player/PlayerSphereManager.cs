using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSphereManager : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform target;
    public Transform sphereParent;
    public GameObject spherePrefab;
    public float radius = 3f;
    public float orbitSpeed = 60f;    // degrees per second
    public float smoothTime = 0.25f;  // higher = less lazy
    public float followSpeed = 5f;
    public bool clockwise = true;


    [Header("Orbiters")]
    public List<Transform> orbiters = new List<Transform>();

    private float currentAngle;
    private float angleVelocity;



    private List<PlayerSphere> spheres;
    public float fireRate = 2f;
    private float fireTimer;
    private float nextFireTime = 0f;
    private int sphereIndex = 0;

    void OnEnable()
    {
        MenuMan.OnNextLevel += SaveOrbiterData;
    }

    void OnDisable()
    {
        MenuMan.OnNextLevel -= SaveOrbiterData;
    }

    void Start()
    {
        spheres ??= new List<PlayerSphere>();

        foreach (Transform s in orbiters)
        {
            PlayerSphere sphere = s.GetComponent<PlayerSphere>();

            if (sphere != null)
            {
                spheres.Add(sphere);
            }
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {

        if (CanShoot()) Shoot();

    }
    private bool CanShoot()
    {
        if (spheres == null || spheres.Count <= 0) return false;

        if (Time.time < nextFireTime)
            return false;

        // Set next fire time
        nextFireTime = Time.time + 1f / fireRate;
        return true;
    }

    private void Shoot()
    {
        spheres[sphereIndex].FireProjectile();
        sphereIndex++;
        if (sphereIndex >= spheres.Count)
            sphereIndex = 0;
    }

    void Update()
    {
        SmoothOrbit();
    }


    private void SmoothOrbit()
    {
        if (!target || orbiters.Count == 0) return;

        // advance the shared orbit angle
        float direction = clockwise ? -1f : 1f;
        currentAngle += orbitSpeed * direction * Time.deltaTime;

        // keep angle in 0–360 range
        if (currentAngle > 360f) currentAngle -= 360f;
        if (currentAngle < 0f) currentAngle += 360f;

        // distribute orbiters evenly around the circle
        float angleStep = 360f / orbiters.Count;

        for (int i = 0; i < orbiters.Count; i++)
        {
            float targetAngle = currentAngle + (angleStep * i);
            Vector2 desiredPos = (Vector2)target.position +
                                 new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad),
                                             Mathf.Sin(targetAngle * Mathf.Deg2Rad)) * radius;

            orbiters[i].position = Vector2.Lerp(orbiters[i].position, desiredPos, 1 - Mathf.Exp(-smoothTime * Time.deltaTime));
        }
    }


    public void AddOrbiter(SphereData data)
    {
        spheres ??= new List<PlayerSphere>();
        
        PlayerSphere newSphere = Instantiate(spherePrefab, sphereParent).GetComponent<PlayerSphere>();
        newSphere.Initialize(data);
        RegisterOrbiter(newSphere.transform);

        fireRate = 1f + fireRate * Mathf.Log10(1f + spheres.Count);
    }

    public void SaveOrbiterData()
    {
        foreach (PlayerSphere s in spheres)
        {
            if (s.data)
                GameState.Instance.RegisterObject(s.data);
        }
    }


    //add an orbiter dynamically
    private void RegisterOrbiter(Transform newOrbiter)
    {
        if (!orbiters.Contains(newOrbiter))
        {
            orbiters.Add(newOrbiter);

            PlayerSphere sphere = newOrbiter.GetComponent<PlayerSphere>();

            if (sphere != null)
            {
                spheres.Add(sphere);
            }
        }

    }

    public void UnregisterOrbiter(Transform orbiter)
    {
        if (orbiters.Contains(orbiter))
            orbiters.Remove(orbiter);

        PlayerSphere sphere = orbiter.GetComponent<PlayerSphere>();

        if (sphere != null)
        {
            if (spheres.Contains(sphere))
                spheres.Remove(sphere);
        }

        Destroy(orbiter);

    }
}
