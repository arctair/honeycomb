using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Town : MonoBehaviour
{
    public GameObject tilePrefab;

    private CoordinateSystem coordinateSystem = new CoordinateSystem();

    void Start()
    {
        float scale1 = 1f / 16f;
        float influence1 = 0.75f;
        float scale2 = 1f / 64f;
        float influence2 = 0.25f;
        int size = 128;
        float clip = 0.625f;
        List<Vector2> hexes = new List<Vector2>();
        for (int q = -size / 2; q < size / 2; q++)
        {
            for (int s = -size / 2; s < size / 2; s++)
            {
                Vector2 coordinate = new Vector2(q, s);
                Vector2 world =
                    coordinateSystem.hexToWorld(coordinate) +
                    new Vector3(size, size, 0);
                Vector2 sample1 = world * scale1;
                Vector2 sample2 = world * scale2;
                if (
                    Mathf.PerlinNoise(sample1.x, sample1.y) * influence1 +
                    Mathf.PerlinNoise(sample2.x, sample2.y) * influence2 >
                    clip
                )
                {
                    hexes.Add (coordinate);
                }
            }
        }

        foreach (Vector3 hex in hexes)
        {
            Instantiate(tilePrefab,
            coordinateSystem.hexToWorld(hex),
            Quaternion.identity);
        }
    }

    void Update()
    {
    }
}
