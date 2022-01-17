using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSystem
{
    public static Vector2 FromAxialToWorld(Vector2 axial)
    {
        return new Vector2((axial[0] + axial[1] / 2) * Mathf.Sqrt(2),
            -axial[1] * Mathf.Sqrt(3) / Mathf.Sqrt(2));
    }
}
