using System.Drawing;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Sprite tile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();

        //sr.sprite = tile;
        Vector2 size = tile.bounds.size;
        bc.size = size;
        bc.offset = tile.bounds.center;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected()
    {
        Debug.Log("Move Indicator have been clicked!");
    }
}
