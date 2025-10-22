using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class ChessPiece : MonoBehaviour
{
    public enum Color { White, Black }
    public Color PieceColor { get; protected set; }
    public string PieceName { get; protected set; }
    public bool HasMoved { get; protected set; } = false; // Useful for pawns and castling
    public bool IsCaptured { get; protected set; } = false;
    public abstract List<Vector2> GetPossibleMoves();
    public (int, int) CurrentTilePosition;

    // Vector2 startPosition,
    public void Init(string name, Color color, (int, int) tilePosition)
    {
        PieceName = name;
        PieceColor = color;
        //CurrentPosition = startPosition;
        gameObject.name = name;
        CurrentTilePosition = tilePosition;
    }
    public void Select()
    {
        Debug.Log($"{PieceName} selected.");
    }

    public void Deselect()
    {
        Debug.Log($"{PieceName} deselected.");
    }
    // Translate from tile cords to pixel cords
    public Vector2 TranslateTilecordToPixelcord(int x, int y)
    {
        float tileSize = 1.28f;
        float offset = tileSize * 3.5f;

        int newX = CurrentTilePosition.Item1 + x;
        int newY = CurrentTilePosition.Item2 + y;

        Vector2 newPixelPos = new Vector2((newX * tileSize) - offset, (newY * tileSize) - offset);

        return newPixelPos;
    }
    public void Move(Vector2 newPosition)
    {
        gameObject.transform.position = newPosition;
    }
}
