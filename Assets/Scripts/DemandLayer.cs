using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandLayer
{
    private CoordinateSystem coordinateSystem;

    private DemandSampler demandSampler;

    private float cellWidth;

    private Texture2D texture;

    private Color[] grid;

    public DemandLayer(
        CoordinateSystem coordinateSystem,
        DemandSampler demandSampler,
        float cellWidth
    )
    {
        this.coordinateSystem = coordinateSystem;
        this.demandSampler = demandSampler;
        this.cellWidth = cellWidth;

        grid = new Color[Screen.width * Screen.height];
        texture =
            new Texture2D(Screen.width,
                Screen.height,
                TextureFormat.ARGB32,
                false);
    }

    public void Draw()
    {
        if (texture.width != Screen.width || texture.height != Screen.height)
        {
            texture =
                new Texture2D(Screen.width,
                    Screen.height,
                    TextureFormat.ARGB32,
                    false);

            grid = new Color[Screen.width * Screen.height];
        }

        foreach (Vector2
            axial
            in
            AxialsInEnvelope(new Rect(0, 0, Screen.width, Screen.height))
        )
        {
            float value = demandSampler.Sample(axial);
            Vector2 center = coordinateSystem.AxialToPixel(axial);
            Color color =
                Color.HSVToRGB(0.125f, 0.25f + value * 0.75f, value * 0.75f);
            foreach (Vector2 point in HexagonPixels())
            {
                Vector2 screenPoint = point + center;
                if (
                    screenPoint.x >= 0 &&
                    screenPoint.x < Screen.width &&
                    screenPoint.y >= 0 &&
                    screenPoint.y < Screen.height
                )
                {
                    grid[Mathf.FloorToInt(screenPoint.y) * Screen.width +
                    Mathf.FloorToInt(screenPoint.x)] = color;
                }
            }
        }

        texture.SetPixels(0, 0, Screen.width, Screen.height, grid, 0);
        texture.Apply();
        Graphics
            .DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }

    private IEnumerable<Vector2> AxialsInEnvelope(Rect envelope)
    {
        for (int q = 0; q <= envelope.width / cellWidth * 4 / 3; q++)
        {
            for (
                int r = 0;
                r <= 2 * envelope.height / cellWidth / Mathf.Sqrt(3);
                r++
            )
            {
                yield return new Vector2(q, r - q / 2);
            }
        }
    }

    private IEnumerable<Vector2> HexagonPixels()
    {
        for (int x = 0; x <= cellWidth; x++)
        {
            float cellHeight = cellWidth / 2 * Mathf.Sqrt(3);
            float heightAtX =
                Mathf.Min(cellHeight, 2 * x * Mathf.Sqrt(3)) -
                Math.Max(0, 2 * (x - cellWidth * 3 / 4) * Mathf.Sqrt(3));
            for (
                int y = Mathf.RoundToInt(cellHeight / 2 - heightAtX / 2);
                y <= Mathf.RoundToInt(cellHeight / 2 + heightAtX / 2);
                y++
            )
            {
                yield return new Vector2(x, y) -
                    new Vector2(cellWidth, cellHeight) / 2;
            }
        }
    }
}
