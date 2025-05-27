using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DrawerCircle : MonoBehaviour
{
    [SerializeField] private int segments = 60;
    
    private float _radius;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.loop = true;
        lr.positionCount = segments;
    }

    public void SetRadius(float radius)
    {
        _radius = radius;

        Draw();
    }
    
    public void SetColor(Color color)
    {
        lr.startColor = color;
        lr.endColor = color;
    }

    private void Draw()
    {
        float angleStep = 360f / segments;
        Vector3[] points = new Vector3[segments];
        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            points[i] = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _radius;
        }
        lr.SetPositions(points);
    }


    
}
