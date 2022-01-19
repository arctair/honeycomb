using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private List<ScreenTile> tiles;

    void Start()
    {
        tiles = ScreenTile.ScreenTiles();
        foreach (ScreenTile tile in tiles)
        {
            tile.gameObject.transform.parent = transform;
            Vector2 world =
                CoordinateSystem.FromAxialToWorld(tile.screenAxialOffset);

            Renderer renderer = tile.gameObject.GetComponent<Renderer>();
            renderer.material.color =
                Color.HSVToRGB(0.125f, 0.75f, Sample(world));
        }
    }

    private Vector2 worldOffset;

    void Update()
    {
        Vector2 axialOffset = CoordinateSystem.FromWorldToAxial(worldOffset);
        Vector2 axialOffsetMod = axialOffset.Mod(1).Add(1).Mod(1);
        foreach (ScreenTile tile in tiles)
        {
            Vector2 screenOffset =
                CoordinateSystem
                    .FromAxialToWorld(tile.screenAxialOffset - axialOffsetMod);
            tile.gameObject.transform.position = screenOffset;
            float sample = Sample(screenOffset + worldOffset);
            tile.gameObject.GetComponent<Renderer>().material.color =
                Color.HSVToRGB(0.125f, 0.75f, sample);
        }
    }

    private float Sample(Vector2 world)
    {
        Vector2 sample = world / 12f + new Vector2(1024, 1024);
        return Mathf.PerlinNoise(sample.x, sample.y) * 4f - 3f;
    }

    private Vector2 lastDragPosition;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastDragPosition;
        worldOffset -= 2 * delta * Camera.main.orthographicSize / Screen.height;
        lastDragPosition = eventData.position;
    }
}
