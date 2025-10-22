using UnityEngine;
using System.Collections.Generic;

public class Rook : ChessPiece
{
    public Sprite whiteRookSprite;
    public Sprite blackRookSprite;

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
