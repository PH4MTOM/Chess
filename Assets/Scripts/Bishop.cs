using UnityEngine;
using System.Collections.Generic;

public class Bishop : ChessPiece
{
    public Sprite whiteBishopSprite;
    public Sprite blackBishopSprite;
    public override void Move(Vector3 newPosition)
    {
        throw new System.NotImplementedException();
    }

    public override List<Vector3> GetPossibleMoves()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (PieceColor == Color.White)
        {
            sr.sprite = whiteBishopSprite;
        }
        else
        {
            sr.sprite = blackBishopSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
