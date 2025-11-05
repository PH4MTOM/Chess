using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public Chessboard board;
    public ChessPieces pieces;
    private ChessPiece selectedPiece;
    public GameObject MoveIndicatorPreFab;
    public Transform parent;
    private Dictionary<(int, int), Vector2> coordsTranslationMap;
    public Dictionary<(int, int), Vector2> CoordsTranslationMap
    {
        get { return coordsTranslationMap; }
    }
    public Dictionary<(int, int), ChessPiece> pieceCoordsMap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board.GenerateBoard();
        pieceCoordsMap = pieces.GeneratePieces();
        GeneratePixelPosTable();
        Debug.Log(pieceCoordsMap[(0,1)].PieceName);
        Debug.Log(pieceCoordsMap[(0,2)]);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(coordsTranslationMap[(0, 0)]);
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            Debug.Log(Mouse.current.position.ReadValue());
            Debug.Log("Mouse Clicked");

            if (hit.collider != null)
            {
                ChessPiece piece = hit.collider.GetComponent<ChessPiece>();
                //Indicator ko = hit.collider.GetComponent<Indicator>();

                Debug.Log("Ray hit!");

                //if (ko != null)
                //{
                //    ko.Selected();
                //}

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
        
        //List<(int, int)> moves = selectedPiece.GetPossibleMoves(pieceCoordsMap);
        foreach ((int, int) item in selectedPiece.GetPossibleMoves(pieceCoordsMap))
        {
            Debug.Log(item);
            GameObject indicator = Instantiate(MoveIndicatorPreFab, coordsTranslationMap[item], Quaternion.identity, parent);
            indicator.name = $"Indicator_{item}";
        }
    }

    // Generate lookup table to translate tile positions to pixel positions
    private void GeneratePixelPosTable()
    {
        float tileSize = 1.28f;
        float offset = tileSize * 3.5f;

        coordsTranslationMap = new Dictionary<(int, int), Vector2>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Vector2 pixelPos = new Vector2((x * tileSize) - offset, (y * tileSize) - offset);
                coordsTranslationMap.Add((x, y), pixelPos);
            }
        }
    }
}
