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
    public abstract List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap);
    public (int, int) CurrentTilePosition;

    public struct MoveData
    {
        public Vector2 pixelPos;
        public (int, int) tilePos;

        public MoveData(Vector2 posA, (int, int) posB)
        {
            pixelPos = posA;
            tilePos = posB;            
        }
    }

    // Vector2 startPosition,
    public void Init(string name, Color color, (int, int) tilePosition)
    {
        PieceName = name;
        PieceColor = color;
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
    public void Move(Vector2 pixelPos, (int, int) tilePos)
    {
        gameObject.transform.position = pixelPos;
        CurrentTilePosition = tilePos;
    }
}
