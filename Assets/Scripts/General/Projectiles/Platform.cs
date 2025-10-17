using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float dropCooldown = 0.5f;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    public void SetDropdown()
    {
        StartCoroutine(DisablePlatform());
    }

    private IEnumerator DisablePlatform()
    {
        effector.rotationalOffset = 180f; // invert collision direction
        yield return new WaitForSeconds(dropCooldown);
        effector.rotationalOffset = 0f;   // restore normal
    }
}
