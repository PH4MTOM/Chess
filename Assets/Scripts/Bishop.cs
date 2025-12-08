using UnityEngine;
using System.Collections.Generic;

public class Bishop : ChessPiece
{
   [SerializeField] private Sprite whiteBishopSprite;
   [SerializeField] private Sprite blackBishopSprite;

    public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
    {
        var possibleMoves = new List<(int, int)> { };
        
        bool topLeftBlocked = false;
        bool topRightBlocked = false;
        bool bottomLeftBlocked = false;
        bool bottomRightBlocked = false;

        for (int i = 1; i <= 7; i++)
        {
            // Top left diagonal moves
            if (CurrentTilePosition.Item1 - i >= 0 && CurrentTilePosition.Item1 - i <= 7 && CurrentTilePosition.Item2 + i >= 0 && CurrentTilePosition.Item2 + i <= 7)
            {
                var topLeftPiece = pieceCoordsMap[(CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2 + i)];
                if (topLeftPiece != null && !topLeftBlocked)
                {
                    if (topLeftPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2 + i));
                        topLeftBlocked = true;
                    }
                    else
                    {
                        topLeftBlocked = true;
                    }
                }
                else if (!topLeftBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2 + i));
                }
            }
            else
            {
                topLeftBlocked = true;
            }

            // Top right diagonal moves
            if (CurrentTilePosition.Item1 + i >= 0 && CurrentTilePosition.Item1 + i <= 7 && CurrentTilePosition.Item2 + i >= 0 && CurrentTilePosition.Item2 + i <= 7)
            {
                var topRightPiece = pieceCoordsMap[(CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2 + i)];
                if (topRightPiece != null && !topRightBlocked)
                {
                    if (topRightPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2 + i));
                        topRightBlocked = true;
                    }
                    else
                    {
                        topRightBlocked = true;
                    }
                }
                else if (!topRightBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2 + i));
                }
            }
            else
            {
                topRightBlocked = true;
            }

            // Bottom left diagonal moves
            if (CurrentTilePosition.Item1 - i >= 0 && CurrentTilePosition.Item1 - i <= 7 && CurrentTilePosition.Item2 - i >= 0 && CurrentTilePosition.Item2 - i <= 7)
            {
                var bottomLeftPiece = pieceCoordsMap[(CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2 - i)];
                if (bottomLeftPiece != null && !bottomLeftBlocked)
                {
                    if (bottomLeftPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2 - i));
                        bottomLeftBlocked = true;
                    }
                    else
                    {
                        bottomLeftBlocked = true;
                    }
                }
                else if (!bottomLeftBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2 - i));
                }
            }
            else
            {
                bottomLeftBlocked = true;
            }


            // Bottom right diagonal moves
            if (CurrentTilePosition.Item1 + i >= 0 && CurrentTilePosition.Item1 + i <= 7 && CurrentTilePosition.Item2 - i >= 0 && CurrentTilePosition.Item2 - i <= 7)
            {
                var bottomRightPiece = pieceCoordsMap[(CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2 - i)];
                if (bottomRightPiece != null && !bottomRightBlocked)
                {
                    if (bottomRightPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2 - i));
                        bottomRightBlocked = true;
                    }
                    else
                    {
                        bottomRightBlocked = true;
                    }
                }
                else if (!bottomRightBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2 - i));
                }
            } 
            else
            {
                bottomRightBlocked = true;
            }


            if (topLeftBlocked && topRightBlocked && bottomLeftBlocked && bottomRightBlocked)
            {
                break;
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
            sr.sprite = whiteBishopSprite;
        }
        else
        {
            sr.sprite = blackBishopSprite;
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
