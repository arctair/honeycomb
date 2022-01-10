using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystem
{
    private float qWidth = Mathf.Cos(Mathf.PI / 6f);

    private float qHeight = Mathf.Sin(Mathf.PI / 6f);

    private float sWidth = Mathf.Cos(5f * Mathf.PI / 6f);

    private float sHeight = Mathf.Sin(5f * Mathf.PI / 6f);

    public Vector3 hexToWorld(Vector2 coordinate)
    {
        return new Vector3(coordinate[0] * qWidth + sWidth * coordinate[1],
            coordinate[0] * qHeight + coordinate[1] * sHeight,
            0);
    }
}
