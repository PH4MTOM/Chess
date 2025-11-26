using System.Collections.Generic;
using UnityEngine;

public class ChessPieces : MonoBehaviour
{
    [SerializeField] private Transform pieceParent;
    [SerializeField] private ChessPiece pawnPreFab;
    [SerializeField] private ChessPiece rookPreFab;
    [SerializeField] private ChessPiece knightPreFab;
    [SerializeField] private ChessPiece bishopPreFab;
    [SerializeField] private ChessPiece queenPreFab;
    [SerializeField] private ChessPiece kingPreFab;

    private float tileSize = 1.28f;
    private string[] pieceSpawnSequence = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" };
    private Dictionary<string, ChessPiece> pieceMap;
    private Dictionary<(int, int), ChessPiece?> initPieceCoordsMap;

    public Dictionary<(int, int), ChessPiece> GeneratePieces() 
    {
        Debug.Log("Generating Chess Pieces...");
        initPieceCoordsMap = new Dictionary<(int, int), ChessPiece?>{};
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
            Vector2 startPositionWhitePawn = new Vector2((x * tileSize) - offset, (1 * tileSize) - offset);
            ChessPiece whitePawn = Instantiate(pawnPreFab, startPositionWhitePawn, Quaternion.identity, pieceParent);
            whitePawn.Init(name: $"WhitePawn_{x}_1", color: ChessPiece.Color.White, tilePosition: (x, 1));
            initPieceCoordsMap.Add((x, 1), whitePawn); // Adds the piece object to the initial chess piece coordination map

            // Create BlackPawn
            Vector2 startPositionBlackPawn = new Vector2((x * tileSize) - offset, (6 * tileSize) - offset);
            ChessPiece blackPawn = Instantiate(pawnPreFab, startPositionBlackPawn, Quaternion.identity, pieceParent);
            blackPawn.Init(name: $"BlackPawn_{x}_6", color: ChessPiece.Color.Black, tilePosition: (x, 6));
            initPieceCoordsMap.Add((x, 6), blackPawn);

            // Create White Pieces (Rook, Knight, Bishop, Queen, King)
            Vector2 startPositionWhite = new Vector2((x * tileSize) - offset, (0 * tileSize) - offset);
            ChessPiece whitePiece = Instantiate(pieceMap[pieceSpawnSequence[x]], startPositionWhite, Quaternion.identity, pieceParent);
            whitePiece.Init(name: $"White{pieceSpawnSequence[x]}_{x}_1", color: ChessPiece.Color.White, tilePosition: (x, 0));
            initPieceCoordsMap.Add((x, 0), whitePiece);

            // Create Black Pieces (Rook, Knight, Bishop, Queen, King)
            Vector2 startPositionBlack = new Vector2((x * tileSize) - offset, (7 * tileSize) - offset);
            ChessPiece blackPiece = Instantiate(pieceMap[pieceSpawnSequence[x]], startPositionBlack, Quaternion.identity, pieceParent);
            blackPiece.Init(name: $"Black{pieceSpawnSequence[x]}_{x}_7", color: ChessPiece.Color.Black, tilePosition: (x, 7));
            initPieceCoordsMap.Add((x, 7), blackPiece);

            // Adding all the empty tiles to the initial piece coordination map
            for (int y = 2; y < 6; y++)
            {
                initPieceCoordsMap.Add((x, y), null);
            }
        }

        return initPieceCoordsMap;
    }
}
