using System.Collections.Generic;
using UnityEngine;

public class ChessPieces : MonoBehaviour
{
    public Transform pieceParent;
    public ChessPiece pawnPreFab;
    public ChessPiece rookPreFab;
    public ChessPiece knightPreFab;
    public ChessPiece bishopPreFab;
    public ChessPiece queenPreFab;
    public ChessPiece kingPreFab;

    private float tileSize = 1.28f;

    string[] pieceSpawnSequence = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" };

    private Dictionary<string, ChessPiece> pieceMap;

    public void GeneratePieces()
    {
        Debug.Log("Generating Chess Pieces...");
        pieceMap = new Dictionary<string, ChessPiece>
        {
            { "Pawn", pawnPreFab },
            { "Rook", rookPreFab },
            { "Knight", knightPreFab },
            { "Bishop", bishopPreFab },
            { "Queen", queenPreFab },
            { "King", kingPreFab }
        };

        // Finding the center of the tile
        float offset = tileSize * 3.5f;
        for (int x = 0; x < 8; x++)
        {
            // Create WhitePawn
            Vector3 startPositionWhitePawn = new Vector2((x * tileSize) - offset, (1 * tileSize) - offset);
            ChessPiece whitePawn = Instantiate(pawnPreFab, startPositionWhitePawn, Quaternion.identity, pieceParent);
            whitePawn.Init(name: $"WhitePawn_{x}_1", color: ChessPiece.Color.White, startPosition: startPositionWhitePawn);

            // Create BlackPawn
            Vector3 startPositionBlackPawn = new Vector2((x * tileSize) - offset, (6 * tileSize) - offset);
            ChessPiece blackPawn = Instantiate(pawnPreFab, startPositionBlackPawn, Quaternion.identity, pieceParent);
            blackPawn.Init(name: $"BlackPawn_{x}_6", color: ChessPiece.Color.Black, startPosition: startPositionBlackPawn);

            // Create White Pieces (Rook, Knight, Bishop, Queen, King)
            Vector3 startPositionWhite = new Vector2((x * tileSize) - offset, (0 * tileSize) - offset);
            ChessPiece whitePiece = Instantiate(pieceMap[pieceSpawnSequence[x]], startPositionWhite, Quaternion.identity, pieceParent);
            whitePiece.Init(name: $"White{pieceSpawnSequence[x]}_{x}_1", color: ChessPiece.Color.White, startPosition: startPositionWhite);

            // Create Black Pieces (Rook, Knight, Bishop, Queen, King)
            Vector3 startPositionBlack = new Vector2((x * tileSize) - offset, (7 * tileSize) - offset);
            ChessPiece blackPiece = Instantiate(pieceMap[pieceSpawnSequence[x]], startPositionBlack, Quaternion.identity, pieceParent);
            blackPiece.Init(name: $"Black{pieceSpawnSequence[x]}_{x}_7", color: ChessPiece.Color.Black, startPosition: startPositionBlack);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
