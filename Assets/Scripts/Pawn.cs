using UnityEngine;
using System.Collections.Generic;
using System;

public class Pawn : ChessPiece
{
    public Sprite whitePawnSprite;
    public Sprite blackPawnSprite;

    public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
    {
        var possibleMoves = new List<(int, int)> { };
        var prePossibleMoves = new List<(int, int)> { };

        void CheckAndAddMove(List<(int, int)> possibleTile)
        {
            foreach ((int, int) posTile in possibleTile)
            {
                var piece = pieceCoordsMap[posTile];
                if (posTile.Item1 != CurrentTilePosition.Item1)
                {
                    // Checking diagonally
                    if (piece != null)
                    {
                        if (piece.PieceColor != PieceColor)
                        {
                            possibleMoves.Add(posTile);
                        }
                    }
                }
                else if (piece == null)
                {
                    possibleMoves.Add(posTile);
                }
            }
        }

        if (PieceColor == Color.White) 
        {
            if (!HasMoved) 
            {
                // Add diagonal moves
                prePossibleMoves.Add((CurrentTilePosition.Item1 + 1, CurrentTilePosition.Item2 + 1));
                prePossibleMoves.Add((CurrentTilePosition.Item1 - 1, CurrentTilePosition.Item2 + 1));

                // Add starting moves
                for (int y = 1; y < 3; y++)
                {
                    if (pieceCoordsMap[(CurrentTilePosition.Item1, CurrentTilePosition.Item2 + y)] != null) 
                    {
                        break;
                    } 
                    else 
                    {
                        prePossibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 + y));
                    }
                }

                CheckAndAddMove(prePossibleMoves);
            }
            else
            {
                // Add diagonal moves
                prePossibleMoves.Add((CurrentTilePosition.Item1 + 1, CurrentTilePosition.Item2 + 1));
                prePossibleMoves.Add((CurrentTilePosition.Item1 - 1, CurrentTilePosition.Item2 + 1));

                // Check if there is any piece infront of the pawn
                prePossibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 + 1));
                CheckAndAddMove(prePossibleMoves);
            }
        }
        else
        {
            if (!HasMoved)
            {
                // Add diagonal moves
                prePossibleMoves.Add((CurrentTilePosition.Item1 + 1, CurrentTilePosition.Item2 - 1));
                prePossibleMoves.Add((CurrentTilePosition.Item1 - 1, CurrentTilePosition.Item2 - 1));

                // Starting Move
                for (int y = 1; y < 3; y--)
                {
                    if (pieceCoordsMap[(CurrentTilePosition.Item1, CurrentTilePosition.Item2 - y)] != null)
                    {
                        break;
                    }
                    else
                    {
                        prePossibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 - y));
                    }
                }

                CheckAndAddMove(prePossibleMoves);
            }
            else
            {
                // Add diagonal moves
                prePossibleMoves.Add((CurrentTilePosition.Item1 + 1, CurrentTilePosition.Item2 - 1));
                prePossibleMoves.Add((CurrentTilePosition.Item1 - 1, CurrentTilePosition.Item2 - 1));

                // Check if there is any piece infront of the pawn
                prePossibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 - 1));
                CheckAndAddMove(prePossibleMoves);
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
            sr.sprite = whitePawnSprite;
        } else
        {
            sr.sprite = blackPawnSprite;
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
