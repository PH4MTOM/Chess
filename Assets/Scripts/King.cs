using UnityEngine;
using System.Collections.Generic;

public class King : ChessPiece
{
    public Sprite whiteKingSprite;
    public Sprite blackKingSprite;

    public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
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
            sr.sprite = whiteKingSprite;
        }
        else
        {
            sr.sprite = blackKingSprite;
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
