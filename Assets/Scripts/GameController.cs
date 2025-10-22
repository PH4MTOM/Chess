using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public Chessboard board;
    public ChessPieces pieces;
    private ChessPiece selectedPiece;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board.GenerateBoard();
        pieces.GeneratePieces();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            Debug.Log(Mouse.current.position.ReadValue());
            Debug.Log("Mouse Clicked");

            if (hit.collider != null)
            {
                ChessPiece piece = hit.collider.GetComponent<ChessPiece>();

                Debug.Log("Ray hit!");

                if (piece != null)
                {
                    SelectPiece(piece);
                }
            }
        }   
    }

    void SelectPiece(ChessPiece piece)
    {
        if (selectedPiece != null)
            selectedPiece.Deselect();

        selectedPiece = piece;
        selectedPiece.Select();
        Debug.Log($"Current PixelPos: {selectedPiece.transform.position.ToString()}, Current TilePos: {selectedPiece.CurrentTilePosition.ToString()}");
        foreach(ChessPiece.MoveData item in selectedPiece.GetPossibleMoves())
        {
            Debug.Log($"Pixel: {item.pixelPos}, Tile: {item.tilePos}");
        }
        // Moving a piece to a new area
        ChessPiece.MoveData firstmove = selectedPiece.GetPossibleMoves()[0];
        selectedPiece.Move(firstmove.pixelPos, firstmove.tilePos);
    }
}
