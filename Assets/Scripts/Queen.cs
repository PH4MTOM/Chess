using UnityEngine;
using System.Collections.Generic;

public class Queen : ChessPiece
{
    [SerializeField] private Sprite whiteQueenSprite;
    [SerializeField] private Sprite blackQueenSprite;

    public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
    {
        var possibleMoves = new List<(int, int)> { };

        bool topLeftBlocked = false;
        bool topRightBlocked = false;
        bool bottomLeftBlocked = false;
        bool bottomRightBlocked = false;
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


            if (topLeftBlocked && topRightBlocked && bottomLeftBlocked && bottomRightBlocked && leftBlocked && topBlocked && rightBlocked && bottomBlocked)
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
            sr.sprite = whiteQueenSprite;
        }
        else
        {
            sr.sprite = blackQueenSprite;
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
