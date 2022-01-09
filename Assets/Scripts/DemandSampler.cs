using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandSampler
{
    public int scale = 128;

    public float floor = 0.75f;

    public float Sample(Vector2 position)
    {
        Vector2 scaled = position / scale;
        float value = Mathf.PerlinNoise(scaled.x, scaled.y);
        return value < floor
            ? 0
            : Mathf.Pow(value - floor, 2) / Mathf.Pow(1 - floor, 2);
    }
}
