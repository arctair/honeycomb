using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoordinateSystemExtensions
{
    public static Vector2 FromAxialToWorld(this Vector2 axial)
    {
        return new Vector2(2 * axial[0] + axial[1], axial[1] * Mathf.Sqrt(3)) /
        Mathf.Sqrt(2);
    }

    public static Vector2 FromWorldToAxial(this Vector2 world)
    {
        return new Vector2(world[0] * Mathf.Sqrt(3) - world[1], 2 * world[1]) /
        Mathf.Sqrt(6);
    }

    public static Vector2 FromScreenToWorld(this Vector3 screen)
    {
        return 2 *
        (
        Input.mousePosition - new Vector3(Screen.width, Screen.height, 0) / 2
        ) /
        Screen.height *
        Camera.main.orthographicSize;
    }

    public static Vector2 AxialRound(this Vector2 axial)
    {
        return axial.FromAxialToCube().CubeRound().FromCubeToAxial();
    }

    public static Vector3 FromAxialToCube(this Vector2 axial)
    {
        return new Vector3(axial[0], axial[1], -axial[0] - axial[1]);
    }

    public static Vector3 CubeRound(this Vector3 cube)
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

    public static Vector2 FromCubeToAxial(this Vector3 cube)
    {
        return new Vector2(cube[0], cube[1]);
    }

    public static Vector3 SetZ(this Vector2 v, float z)
    {
        return new Vector3(v[0], v[1], z);
    }
}
