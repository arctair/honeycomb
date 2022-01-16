using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private DemandLayer demandLayer;

    void Start()
    {
        int cellWidth = 24;
        CoordinateSystem coordinateSystem = new CoordinateSystem(cellWidth);
        demandLayer =
            new DemandLayer(coordinateSystem,
                new DemandSampler(coordinateSystem, 192),
                cellWidth);
    }

    void OnGUI()
    {
        demandLayer.Draw();
    }
}
