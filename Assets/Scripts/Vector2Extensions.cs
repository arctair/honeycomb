using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 Add(this Vector2 v2, float s)
    {
        return new Vector2(v2[0] + s, v2[1] + s);
    }

    public static Vector2 Multiply(this Vector2 v2, float s)
    {
        return new Vector2(v2[0], v2[1]) * s;
    }

    public static Vector2 Add(this Vector2 v1, Vector2 v2)
    {
        return v1 + v2;
    }

    public static Vector2 Divide(this Vector2 v, float s)
    {
        return new Vector2(v[0], v[1]) / s;
    }

    public static Vector2 Mod(this Vector2 v2, float s)
    {
        return new Vector2(v2[0] % s, v2[1] % s);
    }
}
