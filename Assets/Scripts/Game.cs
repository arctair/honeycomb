using System;
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

    private float scale = 2;

    private float scalePow => Mathf.Pow(2, scale);

    private Vector3 scalePow3 => new Vector3(scalePow, scalePow, scalePow);

    void Update()
    {
        scale += Input.mouseScrollDelta[1] / 10;
        Vector2 axialOffsetMod =
            worldOffset.FromWorldToAxial().Mod(1).Add(1).Mod(1);
        foreach (ScreenTile tile in tiles)
        {
            Vector2 screenOffset =
                (tile.screenAxialOffset - axialOffsetMod).FromAxialToWorld() *
                scalePow;
            tile.gameObject.transform.position = screenOffset;
            tile.gameObject.transform.localScale = scalePow3;
            float sample = Sample(screenOffset / scalePow + worldOffset);
            tile.gameObject.GetComponent<Renderer>().material.color =
                Color.HSVToRGB(0.125f, 0.75f, sample);
        }
        cursor.transform.position =
            Input
                .mousePosition
                .FromScreenToWorld()
                .FromWorldToAxial()
                .Divide(scalePow)
                .Add(axialOffsetMod)
                .AxialRound()
                .Add(-axialOffsetMod)
                .Multiply(scalePow)
                .FromAxialToWorld()
                .SetZ(-1);
        cursor.transform.localScale = scalePow3;
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
        worldOffset -=
            2 * delta * Camera.main.orthographicSize / Screen.height / scalePow;
        lastDragPosition = eventData.position;
    }
}
