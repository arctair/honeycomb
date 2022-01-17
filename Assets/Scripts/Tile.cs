using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2 screenAxial;

    public GameObject gameObject;

    public Tile(Vector2 screenAxial, GameObject gameObject)
    {
        this.screenAxial = screenAxial;
        this.gameObject = gameObject;
    }

    public static List<Tile> ScreenTiles()
    {
        Quaternion pointTopRotation =
            Quaternion
                .Euler(90 - Mathf.Atan(1 / Mathf.Sqrt(2)) / Mathf.PI * 180,
                0,
                45);

        int tMax =
            2 *
            Mathf
                .CeilToInt(1 +
                Camera.main.orthographicSize *
                Screen.width /
                Screen.height /
                Mathf.Sqrt(2));

        int rMax =
            2 *
            Mathf
                .FloorToInt(1 +
                Camera.main.orthographicSize * Mathf.Sqrt(2) / Mathf.Sqrt(3));

        List<Tile> tiles = new List<Tile>();
        for (float r = 0; r <= rMax; r++)
        {
            for (int t = 0; t <= tMax; t++)
            {
                Vector2 screenAxial =
                    new Vector2(t -
                        Mathf.FloorToInt((r - rMax / 2 + 1) / 2) -
                        tMax / 2,
                        r - rMax / 2);

                GameObject gameObject =
                    GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject.transform.position =
                    CoordinateSystem
                        .FromAxialToWorld(CoordinateSystem
                            .FromWorldToAxial(CoordinateSystem
                                .FromAxialToWorld(screenAxial)));
                gameObject.transform.rotation = pointTopRotation;

                tiles.Add(new Tile(screenAxial, gameObject));
            }
        }
        return tiles;
    }
}
