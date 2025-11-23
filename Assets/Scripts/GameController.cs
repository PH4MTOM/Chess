using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public Chessboard board;
    public ChessPieces pieces;
    private ChessPiece selectedPiece;
    public Indicator MoveIndicatorPreFab;
    public Transform parent;
    private Dictionary<(int, int), Vector2> coordsTranslationMap;
    public Dictionary<(int, int), Vector2> CoordsTranslationMap
    {
        get { return coordsTranslationMap; }
    }
    public Dictionary<(int, int), ChessPiece?> pieceCoordsMap;
    private List<Indicator> activeIndicators = new List<Indicator>();
    public ChessPiece lastSelectedPiece;
    public Boolean isWhiteTurn = true;
    public Boolean isWhiteKingChecked = false;
    public Boolean isBlackKingChecked = false;
    public (int, int) whiteKingTilePos = (4,0);
    public (int, int) blackKingTilePos = (4,7);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board.GenerateBoard();
        pieceCoordsMap = pieces.GeneratePieces();
        GeneratePixelPosTable();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(coordsTranslationMap[(0, 0)]);
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Creating a hitCollider to detect what object the mouse is clicking
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPos);

            Collider2D selectedCollider = null;

            // Prioritize the hit on indicators
            foreach (Collider2D hit in hits)
            {
                if (hit.GetComponent<Indicator>() != null)
                {
                    selectedCollider = hit;
                    break;
                }
                else if (selectedCollider == null && hit.GetComponent<ChessPiece>() != null)
                {
                    selectedCollider = hit;
                }
            }

            Debug.Log(Mouse.current.position.ReadValue());
            Debug.Log("Mouse Clicked!");
            ChessPiece piece = null;            

            if (selectedCollider != null)
            {
                Indicator indicator = selectedCollider.GetComponent<Indicator>();
                piece = selectedCollider.GetComponent<ChessPiece>();

                Debug.Log("hitCollider collided!");

                // Click on a move indicator
                if (indicator != null)
                {
                    Debug.Log("Indicator have been clicked!");
                    Debug.Log(lastSelectedPiece);
                    MovePiece(lastSelectedPiece, indicator);
                    changeTurn();
                    checkForCheck();
                    RemoveAllIndicators();                    
                } 
                else if (piece != null)
                {
                    RemoveAllIndicators();
                    lastSelectedPiece = piece;
                    SelectPiece(piece);                   
                } 
                else
                {
                    RemoveAllIndicators();
                }           
            }
            else
            {
                RemoveAllIndicators();
            }
        }   
    }

    void changeTurn()
    {
        if (isWhiteTurn)
        {
            isWhiteTurn = false;
        }
        else
        {
            isWhiteTurn = true;
        }
    }

    Boolean checkForCheck(Dictionary<(int, int), ChessPiece?> pieceCoordsMap, (int, int) kingTilePosition)
    {
        var tempMoves = new List<(int, int)> { };
        var moves = new List<(int, int)> { };

        foreach (KeyValuePair<(int, int), ChessPiece?> entry in pieceCoordsMap)
        {
            if (entry.Value != null)
            {
                if (isWhiteTurn)
                {
                    tempMoves = entry.Value.GetPossibleMoves(pieceCoordsMap);

                    if (tempMoves.Contains(kingTilePosition))
                    {
                        Debug.Log("THERE IS A KING!!!");
                    }
                }
                else if (!isWhiteTurn)
                {
                    tempMoves = entry.Value.GetPossibleMoves(pieceCoordsMap);

                    if (tempMoves.Contains(kingTilePosition))
                    {
                        Debug.Log("THERE IS A KING!!!");                        
                    }
                }
            }
            else
            {
                //Debug.Log("Entry value is null.");
            }
        }
        Debug.Log("THERE IS NO KING!!!");

        return false;
    }

    void RemoveAllIndicators()
    {
        for (int i = activeIndicators.Count - 1; i >= 0; i--)
        {
            Destroy(activeIndicators[i].gameObject);
        }
        activeIndicators.Clear();
    }

    Boolean IsChecked(Dictionary<(int, int), ChessPiece?> tempPieceCoordsMap, (int, int) kingTilePosition)
    {
        var tempMoves = new List<(int, int)> { };

        foreach (KeyValuePair<(int, int), ChessPiece?> entry in tempPieceCoordsMap)
        {
            if (entry.Value != null)
            {
                if (isWhiteTurn && entry.Value.PieceColor == ChessPiece.Color.Black)
                {                    
                    tempMoves = entry.Value.GetPossibleMoves(tempPieceCoordsMap);

                    if (tempMoves.Contains(kingTilePosition))
                    {
                        Debug.Log("THERE IS A KING!!!");
                        return true;
                    }
                }
                else if (!isWhiteTurn && entry.Value.PieceColor == ChessPiece.Color.White)
                {
                    
                    tempMoves = entry.Value.GetPossibleMoves(tempPieceCoordsMap);

                    if (tempMoves.Contains(kingTilePosition))
                    {
                        Debug.Log("THERE IS A KING!!!");
                        return true;
                    }
                }
            }                
            else
            {
                //Debug.Log("Entry value is null.");
            }
        }
        Debug.Log("THERE IS NO KING!!!");
        return false;
    }

    void SelectPiece(ChessPiece piece)
    {      
        selectedPiece = piece;          
        selectedPiece.Select();

        if (piece.PieceColor == ChessPiece.Color.White && isWhiteTurn)
        {
            Debug.Log($"Current PixelPos: {selectedPiece.transform.position.ToString()}, Current TilePos: {selectedPiece.CurrentTilePosition.ToString()}");          

            foreach ((int, int) item in selectedPiece.GetPossibleMoves(pieceCoordsMap))
            {
                Debug.Log(item);

                var tempPieceCoordsMap = new Dictionary<(int, int), ChessPiece?>(pieceCoordsMap);
                tempPieceCoordsMap[piece.CurrentTilePosition] = null;
                tempPieceCoordsMap[item] = piece;

                if (piece is King)
                {
                    (int, int) tempWhiteKingTilePos = item;
                    if (IsChecked(tempPieceCoordsMap, tempWhiteKingTilePos))
                    {
                        break;
                    }
                }
                else
                {
                    if (IsChecked(tempPieceCoordsMap, whiteKingTilePos))
                    {
                        break;
                    }                    
                }              

                Indicator indicator = Instantiate(MoveIndicatorPreFab, coordsTranslationMap[item], Quaternion.identity, parent);
                indicator.Init(item);
                activeIndicators.Add(indicator);
                indicator.name = $"Indicator_{item}";
            }
        }
        else if (piece.PieceColor == ChessPiece.Color.Black && !isWhiteTurn)
        {
            Debug.Log($"Current PixelPos: {selectedPiece.transform.position.ToString()}, Current TilePos: {selectedPiece.CurrentTilePosition.ToString()}");            
            
            foreach ((int, int) item in selectedPiece.GetPossibleMoves(pieceCoordsMap))
            {
                Debug.Log(item);

                var tempPieceCoordsMap = new Dictionary<(int, int), ChessPiece?>(pieceCoordsMap);
                tempPieceCoordsMap[piece.CurrentTilePosition] = null;
                tempPieceCoordsMap[item] = piece;

                // Check if black king will be checked by moving this piece (i.e. an illegal move)
                if (piece is King)
                {
                    (int, int) tempBlackKingTilePos = item;
                    if (IsChecked(tempPieceCoordsMap, tempBlackKingTilePos))
                    {
                        continue;
                    }
                }
                else
                {
                    if (IsChecked(tempPieceCoordsMap, blackKingTilePos))
                    {
                        continue;
                    }
                }

                Indicator indicator = Instantiate(MoveIndicatorPreFab, coordsTranslationMap[item], Quaternion.identity, parent);
                indicator.Init(item);
                activeIndicators.Add(indicator);
                indicator.name = $"Indicator_{item}";
            }
        } 
        else
        {
            Debug.Log("Turn-handling went wrong!!!");
        }
    }

    void MovePiece(ChessPiece piece, Indicator indicator)
    {
        Debug.Log($"Current PixelPos of lastSelectedPiece: {piece.transform.position.ToString()}, Current TilePos: {piece.CurrentTilePosition.ToString()}");
        Debug.Log($"Current PixelPos of indicator: {indicator.transform.position.ToString()}, Current TilePos: {indicator.CurrentTilePosition.ToString()}");

        if (pieceCoordsMap[indicator.CurrentTilePosition] != null) // Enemy piece on indicator position
        {
            Debug.Log($"Captured {pieceCoordsMap[indicator.CurrentTilePosition].PieceName}");
            Destroy(pieceCoordsMap[indicator.CurrentTilePosition].gameObject); // Remove enemy piece
        }

        pieceCoordsMap[piece.CurrentTilePosition] = null;
        piece.Move(indicator.transform.position, indicator.CurrentTilePosition);
        pieceCoordsMap[indicator.CurrentTilePosition] = piece;

        if (piece is King && piece.PieceColor == ChessPiece.Color.White)
        {
            whiteKingTilePos = piece.CurrentTilePosition;
        }
        else if (piece is King && piece.PieceColor == ChessPiece.Color.Black)
        {
            blackKingTilePos = piece.CurrentTilePosition;
        }

        Debug.Log("Piece has been moved!");
        RemoveAllIndicators();
        Debug.Log("Indicators has been removed!");
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
