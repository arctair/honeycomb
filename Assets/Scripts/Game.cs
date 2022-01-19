using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private List<ScreenTile> tiles;

    private GameObject cursor;

    void Start()
    {
        TileFactory tileFactory = new TileFactory(Shader.Find("Unlit/Color"));
        tiles = tileFactory.CreateScreen();
        foreach (ScreenTile tile in tiles)
        {
            tile.gameObject.transform.parent = transform;
            float sample = Sample(tile.screenAxialOffset.FromAxialToWorld());
            tile.gameObject.GetComponent<Renderer>().material.color =
                Color.HSVToRGB(0.125f, 0.75f, sample);
        }
        cursor = tileFactory.Create();
        cursor.transform.parent = transform;
        cursor.GetComponent<Renderer>().material.color = Color.green;
    }

    private Vector2 worldOffset;

    void Update()
    {
        Vector2 axialOffsetMod =
            worldOffset.FromWorldToAxial().Mod(1).Add(1).Mod(1);
        foreach (ScreenTile tile in tiles)
        {
            Vector2 screenOffset =
                (tile.screenAxialOffset - axialOffsetMod).FromAxialToWorld();
            tile.gameObject.transform.position = screenOffset;
            float sample = Sample(screenOffset + worldOffset);
            tile.gameObject.GetComponent<Renderer>().material.color =
                Color.HSVToRGB(0.125f, 0.75f, sample);
        }
        cursor.transform.position =
            Input.mousePosition.FromScreenToWorld().SetZ(-1);
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
