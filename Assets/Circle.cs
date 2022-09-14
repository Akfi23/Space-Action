using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class Circle : MonoBehaviour
{
    public void DrawCircle(float radius = 1f, float width = 0.08f)
    {
        var segments = 360;
        var line = GetComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.startWidth = width;
        line.endWidth = width;
        line.positionCount = segments + 1;

        var pointCount = segments + 1;
        var points = new Vector3[pointCount];

        for (var i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        line.SetPositions(points);
    }
}
