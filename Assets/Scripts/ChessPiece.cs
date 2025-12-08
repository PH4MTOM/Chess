using UnityEngine;
using System.Collections.Generic;

public abstract class ChessPiece : MonoBehaviour
{
    public enum Color { White, Black }
    public Color PieceColor { get; private set; }
    public bool HasMoved { get; private set; } = false;
    public abstract List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap);
    public (int, int) CurrentTilePosition { get; private set; }

    public void Init(string name, Color color, (int, int) tilePosition)
    {
        PieceColor = color;
        gameObject.name = name;
        CurrentTilePosition = tilePosition;
    }
    public void Move(Vector2 pixelPos, (int, int) tilePos)
    {
        gameObject.transform.position = pixelPos;
        CurrentTilePosition = tilePos;
        HasMoved = true;
    }
}
