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
}
