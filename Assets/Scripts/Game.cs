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
        for (int r = 0; r <= 32; r++)
        {
            for (int q = 0; q <= 32; q++)
            {
                GameObject tile = CreateTile(new Vector2(q - r / 2, r));
                float sample = Sample(tile.transform.position);
                tile.GetComponent<Renderer>().material.color =
                    Color.HSVToRGB(0.125f, 0.75f, sample);
            }
        }
    }

    private GameObject CreateTile(Vector2 axial)
    {
        Vector2 world =
            new Vector2((axial[0] + axial[1] / 2) * Mathf.Sqrt(2),
                -axial[1] * Mathf.Sqrt(3) / Mathf.Sqrt(2)) -
            new Vector2(20f, -20f);

        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tile.transform.parent = transform;
        tile.transform.SetPositionAndRotation (world, pointTopRotation);
        return tile;
    }

    private float Sample(Vector2 world)
    {
        Vector2 sample = world / 12f + new Vector2(1024, 1024);
        return Mathf.PerlinNoise(sample.x, sample.y) * 4f - 3f;
    }
}
