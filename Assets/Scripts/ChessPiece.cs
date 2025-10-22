using UnityEngine;
using System.Collections.Generic;

public abstract class ChessPiece : MonoBehaviour
{
    public enum Color { White, Black }
    public Color PieceColor { get; protected set; }
    public string PieceName { get; protected set; }
    public Vector2 CurrentPosition { get; protected set; }
    public bool HasMoved { get; protected set; } = false; // Useful for pawns and castling
    public bool IsCaptured { get; protected set; } = false;
    public abstract List<Vector2> GetPossibleMoves();
    public abstract void Move(Vector2 newPosition);

    public void Init(string name, Color color, Vector2 startPosition)
    {
        PieceName = name;
        PieceColor = color;
        CurrentPosition = startPosition;

        gameObject.name = name;
    }
    public void Select()
    {
        Debug.Log($"{PieceName} selected.");
    }

    public void Deselect()
    {
        Debug.Log($"{PieceName} deselected.");
    }
}
