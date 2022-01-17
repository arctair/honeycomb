using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    void Start()
    {
        Shader unlitShader = Shader.Find("Unlit/Color");

        foreach (Tile tile in Tile.ScreenTiles())
        {
            tile.gameObject.transform.parent = transform;
            Vector2 world = CoordinateSystem.FromAxialToWorld(tile.screenAxial);

            Renderer renderer = tile.gameObject.GetComponent<Renderer>();
            renderer.material.shader = unlitShader;
            renderer.material.color =
                Color.HSVToRGB(0.125f, 0.75f, Sample(world));
        }
    }

    private float Sample(Vector2 world)
    {
        Vector2 sample = world / 12f + new Vector2(1024, 1024);
        return Mathf.PerlinNoise(sample.x, sample.y) * 4f - 3f;
    }
}
