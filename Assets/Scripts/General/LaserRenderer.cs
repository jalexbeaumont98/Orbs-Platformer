using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRenderer : MonoBehaviour
{
    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.positionCount = 2;
        lr.enabled = true;
    }

    public void DrawLaser(Vector2 start, Vector2 end)
    {
        lr.enabled = true;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    public void DisableLaser()
    {
        lr.enabled = false;
    }
}
