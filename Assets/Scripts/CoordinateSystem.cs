using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystem
{
    private float cellHalfWidth;

    public CoordinateSystem(float cellWidth)
    {
        this.cellHalfWidth = cellWidth / 2f;
    }

    public Vector2 AxialToPixel(Vector2 axial)
    {
        Vector3 cube = AxialToCube(axial);
        float q = cube[0];
        float r = cube[1];
        float s = cube[2];
        return new Vector2(q * 3 / 2,
            q * Mathf.Sqrt(3) / 2 + r * Mathf.Sqrt(3)) *
        cellHalfWidth;
    }

    public Vector2 PixelToAxial(Vector2 pixel)
    {
        return AxialRound(new Vector2((pixel.x * 2 / 3),
            (pixel.x * -1 / 3 + pixel.y * Mathf.Sqrt(3) / 3)) /
        cellHalfWidth);
    }

    public Vector3 AxialToCube(Vector2 axial)
    {
        float q = axial[0];
        float r = axial[1];
        return new Vector3(q, r, -q - r);
    }

    public Vector2 CubeToAxial(Vector3 cube)
    {
        return new Vector2(cube[0], cube[1]);
    }

    public Vector2 AxialRound(Vector2 axial)
    {
        return CubeToAxial(CubeRound(AxialToCube(axial)));
    }

    public Vector3 CubeRound(Vector3 cube)
    {
        float q = Mathf.Round(cube[0]);
        float r = Mathf.Round(cube[1]);
        float s = Mathf.Round(cube[2]);
        float dq = Mathf.Abs(q - cube[0]);
        float dr = Mathf.Abs(r - cube[1]);
        float ds = Mathf.Abs(s - cube[2]);

        if (dq > dr && dq > ds)
        {
            q = -r - s;
        }
        else if (dr > ds)
        {
            r = -q - s;
        }
        else
        {
            s = -q - r;
        }

        return new Vector3(q, r, s);
    }
}
