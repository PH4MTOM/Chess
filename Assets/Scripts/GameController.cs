using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private Chessboard board;
    [SerializeField] private ChessPieces pieces;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Text winnerText;
    [SerializeField] private Indicator MoveIndicatorPreFab;
    [SerializeField] private Transform parent;
    
    private Dictionary<(int, int), ChessPiece?> pieceCoordsMap;
    public Dictionary<(int, int), Vector2> coordsTranslationMap { get; private set; }
    private List<Indicator> activeIndicators = new List<Indicator>();
    private ChessPiece lastSelectedPiece;
    private Boolean isWhiteTurn = true;
    private (int, int) whiteKingTilePos = (4,0);
    private (int, int) blackKingTilePos = (4,7);

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

            ChessPiece piece = null;            

            if (selectedCollider != null)
            {
                Indicator indicator = selectedCollider.GetComponent<Indicator>();
                piece = selectedCollider.GetComponent<ChessPiece>();

                // Click on a move indicator
                if (indicator != null)
                {
                    MovePiece(lastSelectedPiece, indicator);
                    changeTurn();
                    if (isCheckmate(pieceCoordsMap))
                    {
                        GameOver();
                    }
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

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        if (isWhiteTurn)
        {
            winnerText.text = "Checkmate!!! Black Won!";
        }
        else
        {
            winnerText.text = "Checkmate!!! White Won!";
        }        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void changeTurn()
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

    Boolean isCheckmate(Dictionary<(int, int), ChessPiece?> pieceCoordsMap)
    {
        var filteredMoves = new List<(int, int)> { };

        foreach (KeyValuePair<(int, int), ChessPiece?> entry in pieceCoordsMap)
        {
            if (entry.Value != null)
            {
                if (isWhiteTurn && entry.Value.PieceColor == ChessPiece.Color.White)
                {
                    filteredMoves = filterMoves(pieceCoordsMap, entry.Value);

                    if (filteredMoves.Count() > 1)
                    {
                        return false;
                    }
                }

                if (!isWhiteTurn && entry.Value.PieceColor == ChessPiece.Color.Black)
                {
                    filteredMoves = filterMoves(pieceCoordsMap, entry.Value);

                    if (filteredMoves.Count() > 1)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
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
                        return true;
                    }
                }
                else if (!isWhiteTurn && entry.Value.PieceColor == ChessPiece.Color.White)
                {
                    
                    tempMoves = entry.Value.GetPossibleMoves(tempPieceCoordsMap);

                    if (tempMoves.Contains(kingTilePosition))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    List<(int, int)> filterMoves(Dictionary<(int, int), ChessPiece?> pieceCoordMap, ChessPiece piece)
    {
        var filteredCoordList = new List<(int, int)> { };

        foreach ((int, int) item in piece.GetPossibleMoves(pieceCoordsMap))
        {
            var tempPieceCoordsMap = new Dictionary<(int, int), ChessPiece?>(pieceCoordsMap);
            tempPieceCoordsMap[piece.CurrentTilePosition] = null;
            tempPieceCoordsMap[item] = piece;

            if (piece.PieceColor == ChessPiece.Color.White)
            {
                if (piece is King)
                {
                    (int, int) tempWhiteKingTilePos = item;
                    if (IsChecked(tempPieceCoordsMap, tempWhiteKingTilePos))
                    {
                        continue;
                    }
                }
                else
                {
                    if (IsChecked(tempPieceCoordsMap, whiteKingTilePos))
                    {
                        continue;
                    }
                }
            } 
            else
            {
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
            }
   
            // Adding the move to the filtered list
            filteredCoordList.Add(item);
        }

        return filteredCoordList;
    }

    void SelectPiece(ChessPiece piece)
    {               
        //piece.Select();

        if (piece.PieceColor == ChessPiece.Color.White && isWhiteTurn)
        {
            foreach ((int, int) item in filterMoves(pieceCoordsMap, piece))
            {
                Indicator indicator = Instantiate(MoveIndicatorPreFab, coordsTranslationMap[item], Quaternion.identity, parent);
                indicator.Init(item);
                activeIndicators.Add(indicator);
                indicator.name = $"Indicator_{item}";
            }
        }
        else if (piece.PieceColor == ChessPiece.Color.Black && !isWhiteTurn)
        {
            foreach ((int, int) item in filterMoves(pieceCoordsMap, piece))
            {
                Indicator indicator = Instantiate(MoveIndicatorPreFab, coordsTranslationMap[item], Quaternion.identity, parent);
                indicator.Init(item);
                activeIndicators.Add(indicator);
                indicator.name = $"Indicator_{item}";
            }
        }
    }

    void MovePiece(ChessPiece piece, Indicator indicator)
    {
        if (pieceCoordsMap[indicator.CurrentTilePosition] != null) // Enemy piece on indicator position
        {
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

        RemoveAllIndicators();
    }

    // Generate lookup table to translate tile positions to pixel positions
    private void GeneratePixelPosTable()
    {
        float offset = board.tileSize * 3.5f;

        coordsTranslationMap = new Dictionary<(int, int), Vector2>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Vector2 pixelPos = new Vector2((x * board.tileSize) - offset, (y * board.tileSize) - offset);
                coordsTranslationMap.Add((x, y), pixelPos);
            }
        }
    }
}
