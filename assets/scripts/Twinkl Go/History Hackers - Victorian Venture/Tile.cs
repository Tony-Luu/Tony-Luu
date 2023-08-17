using System;
using UnityEngine;

[Serializable]
public class Tile : MonoBehaviour
{
    [SerializeField] private int TileID;
    [SerializeField] private int TileXArrayPosition;
    [SerializeField] private int TileYArrayPosition;
    [SerializeField] private Vector2 TilePosition;
    [SerializeField] private GameObject TileObject;


    public int ReturnTileID()
    {
        return TileID;
    }

    public int ReturnXArrayPosition()
    {
        return TileXArrayPosition;
    }

    public int ReturnYArrayPosition()
    {
        return TileYArrayPosition;
    }

    public Vector2 ReturnTilePosition()
    {
        return TilePosition;
    }

    public GameObject ReturnTileObject()
    {
        return TileObject;
    }

    public void SetNewTileID(int NewTileID)
    {
        TileID = NewTileID;
    }

    public void SetTileArrayPositions(int X, int Y)
    {
        TileXArrayPosition = X;
        TileYArrayPosition = Y;
    }

    public void SetTilePositions(float xPosition, float yPosition)
    {
        TilePosition.x = xPosition;
        TilePosition.y = yPosition;
        TileObject.transform.localPosition = TilePosition;
    }

    public void SetNewTileObject(GameObject NewTile)
    {
        TileObject = NewTile;
    }

}
