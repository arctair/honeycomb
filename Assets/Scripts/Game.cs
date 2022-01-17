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

    private Vector2 quadrantDimensions;

    private Shader unlitShader;

    void Start()
    {
        unlitShader = Shader.Find("Unlit/Color");

        int qMax =
            Mathf
                .CeilToInt(Camera.main.orthographicSize *
                Screen.width /
                Screen.height /
                Mathf.Sqrt(2));

        int rMax =
            Mathf
                .FloorToInt(Camera.main.orthographicSize *
                Mathf.Sqrt(2) /
                Mathf.Sqrt(3));

        for (float r = -rMax; r <= rMax; r++)
        {
            for (int q = -qMax; q <= qMax; q++)
            {
                GameObject tile =
                    CreateTile(new Vector2(q - Mathf.FloorToInt((r + 1) / 2),
                        r));
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
                -axial[1] * Mathf.Sqrt(3) / Mathf.Sqrt(2));

        GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tile.transform.parent = transform;
        tile.transform.SetPositionAndRotation (world, pointTopRotation);
        tile.GetComponent<Renderer>().material.shader = unlitShader;
        return tile;
    }

    private float Sample(Vector2 world)
    {
        Vector2 sample = world / 12f + new Vector2(1024, 1024);
        return Mathf.PerlinNoise(sample.x, sample.y) * 4f - 3f;
    }
}
