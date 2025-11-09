using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Rook : ChessPiece
{
    public Sprite whiteRookSprite;
    public Sprite blackRookSprite;

    public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
    {
        var possibleMoves = new List<(int, int)> { };        

        // Adds all tiles left of the rook
        for (int x = CurrentTilePosition.Item1 - 1; x >= 0; x--)
        {
            var piece = pieceCoordsMap[(x, CurrentTilePosition.Item2)];
            if (piece != null)
            {
                if (piece.PieceColor != PieceColor)
                {
                    possibleMoves.Add((x, CurrentTilePosition.Item2));
                    break;
                } 
                else
                {
                    break;
                }
            }
            else
            {
                possibleMoves.Add((x, CurrentTilePosition.Item2));
            }
        }
        // Adds all tiles right of the rook
        for (int x = CurrentTilePosition.Item1 + 1; x <= 7; x++)
        {
            var piece = pieceCoordsMap[(x, CurrentTilePosition.Item2)];
            if (piece != null)
            {
                if (piece.PieceColor != PieceColor)
                {
                    possibleMoves.Add((x, CurrentTilePosition.Item2));
                    break;
                }
                else
                {
                    break;
                }
            }
            else
            {
                possibleMoves.Add((x, CurrentTilePosition.Item2));
            }
        }
        // Adds all tiles downward of the rook
        for (int y = CurrentTilePosition.Item2 - 1; y >= 0; y--)
        {
            var piece = pieceCoordsMap[(CurrentTilePosition.Item1, y)];
            if (piece != null)
            {
                if (piece.PieceColor != PieceColor)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1, y));
                    break;
                }
                else
                {
                    break;
                }
            }
            else
            {
                possibleMoves.Add((CurrentTilePosition.Item1, y));
            }
        }
        // Adds all tiles upward of the rook
        for (int y = CurrentTilePosition.Item2 + 1; y <= 7; y++)
        {
            var piece = pieceCoordsMap[(CurrentTilePosition.Item1, y)];
            if (piece != null)
            {
                if (piece.PieceColor != PieceColor)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1, y));
                    break;
                }
                else
                {
                    break;
                }
            }
            else
            {
                possibleMoves.Add((CurrentTilePosition.Item1, y));
            }
        }

        return possibleMoves;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        if (PieceColor == Color.White)
        {
            sr.sprite = whiteRookSprite;
        }
        else
        {
            sr.sprite = blackRookSprite;
        }

        if (sr.sprite != null)
        {
            Vector2 size = sr.sprite.bounds.size;
            bc.size = size;
            bc.offset = sr.sprite.bounds.center;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
