using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    [SerializeField] private Sprite whiteRookSprite;
    [SerializeField] private Sprite blackRookSprite;

    public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
    {
        var possibleMoves = new List<(int, int)> { };

        bool leftBlocked = false;
        bool rightBlocked = false;
        bool topBlocked = false;
        bool bottomBlocked = false;

        for (int i = 1; i <= 7; i++)
        {
            // Left moves
            if (CurrentTilePosition.Item1 - i >= 0 && CurrentTilePosition.Item1 - i <= 7 && CurrentTilePosition.Item2 >= 0 && CurrentTilePosition.Item2 <= 7)
            {
                var leftPiece = pieceCoordsMap[(CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2)];
                if (leftPiece != null && !leftBlocked)
                {
                    if (leftPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2));
                        leftBlocked = true;
                    }
                    else
                    {
                        leftBlocked = true;
                    }
                }
                else if (!leftBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1 - i, CurrentTilePosition.Item2));
                }
            }
            else
            {
                leftBlocked = true;
            }

            // Top moves
            if (CurrentTilePosition.Item1 >= 0 && CurrentTilePosition.Item1 <= 7 && CurrentTilePosition.Item2 + i >= 0 && CurrentTilePosition.Item2 + i <= 7)
            {
                var topPiece = pieceCoordsMap[(CurrentTilePosition.Item1, CurrentTilePosition.Item2 + i)];
                if (topPiece != null && !topBlocked)
                {
                    if (topPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 + i));
                        topBlocked = true;
                    }
                    else
                    {
                        topBlocked = true;
                    }
                }
                else if (!topBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 + i));
                }
            }
            else
            {
                topBlocked = true;
            }

            // Right moves
            if (CurrentTilePosition.Item1 + i >= 0 && CurrentTilePosition.Item1 + i <= 7 && CurrentTilePosition.Item2 >= 0 && CurrentTilePosition.Item2 <= 7)
            {
                var rightPiece = pieceCoordsMap[(CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2)];
                if (rightPiece != null && !rightBlocked)
                {
                    if (rightPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2));
                        rightBlocked = true;
                    }
                    else
                    {
                        rightBlocked = true;
                    }
                }
                else if (!rightBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1 + i, CurrentTilePosition.Item2));
                }
            }
            else
            {
                rightBlocked = true;
            }

            // Bottom moves
            if (CurrentTilePosition.Item1 >= 0 && CurrentTilePosition.Item1 <= 7 && CurrentTilePosition.Item2 - i >= 0 && CurrentTilePosition.Item2 - i <= 7)
            {
                var bottomPiece = pieceCoordsMap[(CurrentTilePosition.Item1, CurrentTilePosition.Item2 - i)];
                if (bottomPiece != null && !bottomBlocked)
                {
                    if (bottomPiece.PieceColor != PieceColor)
                    {
                        possibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 - i));
                        bottomBlocked = true;
                    }
                    else
                    {
                        bottomBlocked = true;
                    }
                }
                else if (!bottomBlocked)
                {
                    possibleMoves.Add((CurrentTilePosition.Item1, CurrentTilePosition.Item2 - i));
                }
            }
            else
            {
                bottomBlocked = true;
            }

            if (leftBlocked && topBlocked && rightBlocked && bottomBlocked)
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
