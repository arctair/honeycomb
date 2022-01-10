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
        List<Vector2> hexes =
            new List<Vector2>()
            { new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0) };

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
