using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGrid : MonoBehaviour
{
    public Color color = Color.red;

    public float cellSize = 8;

    void Start()
    {
        CoordinateSystem coordinateSystem = new CoordinateSystem(cellSize);
        DemandSampler demandSampler = new DemandSampler();

        int width = 2048;
        int height = 1024;

        Texture2D texture =
            new Texture2D(width, height, TextureFormat.ARGB32, false);
        Sprite sprite =
            Sprite
                .Create(texture,
                new Rect(0, 0, width, height),
                new Vector2(0.5f, 0.5f));
        GetComponent<SpriteRenderer>().sprite = sprite;

        Color[] grid = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 hexCenter =
                    coordinateSystem
                        .AxialToPixel(coordinateSystem
                            .PixelToAxial(new Vector2(x, y)));
                float value = demandSampler.Sample(hexCenter);
                grid[y * width + x] = value > 0 ? color : Color.clear;
            }
        }

        texture.SetPixels(0, 0, width, height, grid, 0);
        texture.Apply();
    }
}
