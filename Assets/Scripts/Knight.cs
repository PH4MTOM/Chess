using UnityEngine;
using System.Collections.Generic;

public class Knight : ChessPiece
{
    public Sprite whiteKnightSprite;
    public Sprite blackKnightSprite;

    public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
    {
        var possibleMoves = new List<(int, int)> { };
        var prePossibleMoves = new List<(int, int)> { };

        void CheckAndAddMove(List<(int, int)> possibleTile)
        {
            foreach ((int, int) posTile in possibleTile)
            {
                // The boundaries of the board.
                if (posTile.Item1 >= 0 && posTile.Item1 <= 7 && posTile.Item2 >= 0 && posTile.Item2 <= 7)
                {
                    var piece = pieceCoordsMap[posTile];
                    if (piece != null)
                    {
                        if (piece.PieceColor != PieceColor)
                        {
                            possibleMoves.Add(posTile);
                        }
                    } 
                    else
                    {
                        possibleMoves.Add(posTile);
                    }
                }
            }
        }

        // Adding upper part of the knights moves
        prePossibleMoves.Add((CurrentTilePosition.Item1 + 1, CurrentTilePosition.Item2 + 2));
        prePossibleMoves.Add((CurrentTilePosition.Item1 + 2, CurrentTilePosition.Item2 + 1));

        prePossibleMoves.Add((CurrentTilePosition.Item1 - 1, CurrentTilePosition.Item2 + 2));
        prePossibleMoves.Add((CurrentTilePosition.Item1 - 2, CurrentTilePosition.Item2 + 1));

        // Adding the lower part of the knights moves
        prePossibleMoves.Add((CurrentTilePosition.Item1 + 1, CurrentTilePosition.Item2 - 2));
        prePossibleMoves.Add((CurrentTilePosition.Item1 + 2, CurrentTilePosition.Item2 - 1));

        prePossibleMoves.Add((CurrentTilePosition.Item1 - 1, CurrentTilePosition.Item2 - 2));
        prePossibleMoves.Add((CurrentTilePosition.Item1 - 2, CurrentTilePosition.Item2 - 1));

        CheckAndAddMove(prePossibleMoves);

        return possibleMoves;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        if (PieceColor == Color.White)
        {
            sr.sprite = whiteKnightSprite;
        }
        else
        {
            sr.sprite = blackKnightSprite;
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
