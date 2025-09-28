using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public abstract class ChessPiece : MonoBehaviour
{
    public enum Color { White, Black }
    public Color PieceColor { get; protected set; }
    public Vector3 CurrentPosition { get; protected set; }
    public bool HasMoved { get; protected set; } = false; // Useful for pawns and castling
    public bool IsCaptured { get; protected set; } = false;
    public abstract List<Vector3> GetPossibleMoves();
    public abstract void Move(Vector3 newPosition);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
