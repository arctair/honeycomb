using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private List<Tile> tiles;

    void Start()
    {
        tiles = Tile.ScreenTiles();
        foreach (Tile tile in tiles)
        {
            tile.gameObject.transform.parent = transform;
            Vector2 world = CoordinateSystem.FromAxialToWorld(tile.screenAxial);

            Renderer renderer = tile.gameObject.GetComponent<Renderer>();
            renderer.material.color =
                Color.HSVToRGB(0.125f, 0.75f, Sample(world));
        }
    }

    private Vector2 worldOffset;

    void Update()
    {
        Vector2 axialOffset = CoordinateSystem.FromWorldToAxial(worldOffset);
        Vector2 axialOffsetFloored =
            new Vector2(Mathf.Floor(axialOffset[0]),
                Mathf.Floor(axialOffset[1]));
        Vector2 axialOffsetMod =
            new Vector2((axialOffset[0] % 1 + 1) % 1,
                (axialOffset[1] % 1 + 1) % 1);
        foreach (Tile tile in tiles)
        {
            tile.gameObject.transform.position =
                CoordinateSystem
                    .FromAxialToWorld(tile.screenAxial - axialOffsetMod);

            Vector2 world =
                CoordinateSystem
                    .FromAxialToWorld(tile.screenAxial + axialOffsetFloored);
            tile.gameObject.GetComponent<Renderer>().material.color =
                Color.HSVToRGB(0.125f, 0.75f, Sample(world));
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
