using UnityEngine;
using System.Collections.Generic;

public class Knight : ChessPiece
{
    public Sprite whiteKnightSprite;
    public Sprite blackKnightSprite;
    public override void Move(Vector2 newPosition)
    {
        throw new System.NotImplementedException();
    }

    public override List<Vector2> GetPossibleMoves()
    {
        throw new System.NotImplementedException();
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
