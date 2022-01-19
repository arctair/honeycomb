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
}
