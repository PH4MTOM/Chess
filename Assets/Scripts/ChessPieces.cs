using System.Collections.Generic;
using UnityEngine;

public class ChessPieces : MonoBehaviour
{
    public Sprite whitePawnSprite;
    public Sprite blackPawnSprite;
    public Sprite whiteRookSprite;
    public Sprite blackRookSprite;
    public Sprite whiteBishopSprite;
    public Sprite blackBishopSprite;
    public Sprite whiteKnightSprite;
    public Sprite blackKnightSprite;
    public Sprite whiteQueenSprite;
    public Sprite blackQueenSprite;
    public Sprite whiteKingSprite;
    public Sprite blackKingSprite;
    public GameObject piecePreFab;
    public Transform pieceParent;

    private float tileSize = 1.28f;
    private enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King };
    private enum PieceColor { White, Black };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GeneratePieces();
    }

    string[] spriteWhite = { "whiteRookSprite", "whiteKnightSprite", "whiteBishopSprite", "whiteQueenSprite",
        "whiteKingSprite", "whiteBishopSprite", "whiteKnightSprite", "whiteRookSprite" };

    string[] spriteBlack = { "blackRookSprite", "blackKnightSprite", "blackBishopSprite", "blackQueenSprite",
        "blackKingSprite", "blackBishopSprite", "blackKnightSprite", "blackRookSprite" };

    private Dictionary<string, Sprite> spriteMap;

    private void Awake()
    {
        spriteMap = new Dictionary<string, Sprite>
        {
            { "whiteRookSprite", whiteRookSprite },
            { "whiteKnightSprite", whiteKnightSprite },
            { "whiteBishopSprite", whiteBishopSprite },
            { "whiteQueenSprite", whiteQueenSprite },
            { "whiteKingSprite", whiteKingSprite },
            { "blackRookSprite", blackRookSprite },
            { "blackKnightSprite", blackKnightSprite },
            { "blackBishopSprite", blackBishopSprite },
            { "blackQueenSprite", blackQueenSprite },
            { "blackKingSprite", blackKingSprite }
        };
    }

    // Need to fix this.
    //Sprite[] spriteWhite = { whitePawnSprite, whiteKnightSprite, whiteBishopSprite, whiteQueenSprite, 
    //                        whiteKingSprite, whiteBishopSprite, whiteKnightSprite, whiteRookSprite };

    void GeneratePieces()
    {
        float offset = tileSize * 3.5f;
        //Generation White and Black Pawns
        for (int x = 0; x < 8; x++)
        {
            //White Pawn
            Vector3 startPositionWhitePawn = new Vector3((x * tileSize) - offset, (1 * tileSize) - offset);
            GameObject whitePawn = Instantiate(piecePreFab, startPositionWhitePawn, Quaternion.identity, pieceParent);

            whitePawn.name = $"WhitePawn_{x}_1";
            whitePawn.GetComponent<SpriteRenderer>().sprite = whitePawnSprite;

            //Black Pawn
            Vector3 startPositionBlackPawn = new Vector3((x * tileSize) - offset, (6 * tileSize) - offset);
            GameObject blackPawn = Instantiate(piecePreFab, startPositionBlackPawn, Quaternion.identity, pieceParent);

            blackPawn.name = $"BlackPawn_{x}_6";
            blackPawn.GetComponent<SpriteRenderer>().sprite = blackPawnSprite;

            //White Pieces (Rook, Knight, Bishop, Queen, King)
            Vector3 startPositionWhite = new Vector3((x * tileSize) - offset, (0 * tileSize) - offset);
            GameObject whitePieces = Instantiate(piecePreFab, startPositionWhite, Quaternion.identity, pieceParent);
            whitePieces.name = $"{spriteWhite[x]}_{x}_0";
            Sprite currentSpriteWhite = spriteMap[spriteWhite[x]];
            whitePieces.GetComponent<SpriteRenderer>().sprite = currentSpriteWhite;

            //Black Pieces (Rook, Knight, Bishop, Queen, King)
            Vector3 startPositionBlack = new Vector3((x * tileSize) - offset, (7 * tileSize) - offset);
            GameObject blackPieces = Instantiate(piecePreFab, startPositionBlack, Quaternion.identity, pieceParent);
            blackPieces.name = $"{spriteBlack[x]}_{x}_7";
            Sprite currentSpriteBlack = spriteMap[spriteBlack[x]];
            blackPieces.GetComponent<SpriteRenderer>().sprite = currentSpriteBlack;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
