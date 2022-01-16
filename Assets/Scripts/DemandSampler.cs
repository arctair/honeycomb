using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandSampler
{
    private CoordinateSystem coordinateSystem;

    private int noiseScale;

    public DemandSampler(CoordinateSystem coordinateSystem, int noiseScale)
    {
        this.coordinateSystem = coordinateSystem;
        this.noiseScale = noiseScale;
    }

    public float Sample(Vector2 axial)
    {
        Vector2 point = coordinateSystem.AxialToPixel(axial) / noiseScale;
        return 3 * Mathf.PerlinNoise(point.x, point.y) - 2;
    }
}
