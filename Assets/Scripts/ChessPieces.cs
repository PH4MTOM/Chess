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

    string[] spriteBlack = { "blackRookSprite", "blackKnightSprite", "blackBishopSprite", "blackQueenSprite",
        "blackKingSprite", "blackBishopSprite", "blackKnightSprite", "blackRookSprite" };

    //string[] spriteWhite = { "whiteRookSprite", "whiteKnightSprite", "whiteBishopSprite", "whiteQueenSprite",
    //    "whiteKingSprite", "whiteBishopSprite", "whiteKnightSprite", "whiteRookSprite" };

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
            //Vector3 startPositionWhite = new Vector3((x * tileSize) - offset, (0 * tileSize) - offset);

            //GameObject whitePieces = Instantiate(piecePreFab, startPositionWhite, Quaternion.identity, pieceParent);
            //whitePieces.name = $"{spriteWhite[x]}_{x}_7";
            //whitePieces.GetComponent<SpriteRenderer>().sprite = spriteWhite[x];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
