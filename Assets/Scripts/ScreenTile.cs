using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTile
{
    public Vector2 screenAxialOffset;

    public GameObject gameObject;

    public ScreenTile(Vector2 screenAxialOffset, GameObject gameObject)
    {
        this.screenAxialOffset = screenAxialOffset;
        this.gameObject = gameObject;
    }

    public static List<ScreenTile> ScreenTiles()
    {
        Shader unlitShader = Shader.Find("Unlit/Color");
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

        List<ScreenTile> tiles = new List<ScreenTile>();
        for (float r = 0; r <= rMax; r++)
        {
            for (int t = 0; t <= tMax; t++)
            {
                Vector2 screenAxialOffset =
                    new Vector2(t -
                        Mathf.FloorToInt((r - rMax / 2 + 1) / 2) -
                        tMax / 2,
                        r - rMax / 2);

                GameObject gameObject =
                    GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject.transform.position =
                    CoordinateSystem.FromAxialToWorld(screenAxialOffset);
                gameObject.transform.rotation = pointTopRotation;
                gameObject.GetComponent<Renderer>().material.shader =
                    unlitShader;

                tiles.Add(new ScreenTile(screenAxialOffset, gameObject));
            }
        }
        return tiles;
    }
}
