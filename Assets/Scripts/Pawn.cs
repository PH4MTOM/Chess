using UnityEngine;
using System.Collections.Generic;

public class Pawn : ChessPiece
{
    public Sprite whitePawnSprite;
    public Sprite blackPawnSprite;

    public override List<Vector2> GetPossibleMoves()
    {
        // Not working. Need to use the Move Function Instead.
        List<Vector2> moves = new List<Vector2>();

        if (PieceColor == Color.White)
        {
            // Move 1 step forward
            moves.Add(TranslateTilecordToPixelcord(0, 1));

            // Move 2 steps if coming from it starting position
            if (CurrentTilePosition.Item2 == 1)
            {
                moves.Add(TranslateTilecordToPixelcord(0, 2));
            }

            // Move up and right
            moves.Add(TranslateTilecordToPixelcord(1, 1));

            // Move up and left
            moves.Add(TranslateTilecordToPixelcord(-1, 1));
        } 
        else
        {
            // Move 1 step forward
            moves.Add(TranslateTilecordToPixelcord(0, -1));

            // Move 2 steps if coming from it starting position
            if (CurrentTilePosition.Item2 == 6)
            {
                moves.Add(TranslateTilecordToPixelcord(0, -2));
            }

            // Move up and right
            moves.Add(TranslateTilecordToPixelcord(-1, -1));

            // Move up and left
            moves.Add(TranslateTilecordToPixelcord(1, -1));
        }
        return moves;
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