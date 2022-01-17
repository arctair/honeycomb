using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private Quaternion
        pointTopRotation =
            Quaternion
                .Euler(90 - Mathf.Atan(1 / Mathf.Sqrt(2)) / Mathf.PI * 180,
                0,
                45);

    void Start()
    {
        CreateTile(new Vector2(0, 0));
        CreateTile(new Vector2(1, 0));
        CreateTile(new Vector2(1, -1));
    }

    private GameObject CreateTile(Vector2 axial)
    {
        Vector2 world =
            new Vector2((axial[0] + axial[1] / 2) * Mathf.Sqrt(2),
                -axial[1] * Mathf.Sqrt(3) / Mathf.Sqrt(2));

        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tile.transform.SetPositionAndRotation (world, pointTopRotation);
        return tile;
    }
}
