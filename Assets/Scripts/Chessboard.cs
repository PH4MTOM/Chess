using UnityEngine;

public class Chessboard : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform boardParent;
    public Sprite lightBrownTileSprite;
    public Sprite darkBrownTileSprite;
    private float tileSize = 1.28f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Vector3 position = new Vector3((x - 3.5f) * tileSize, (y - 3.5f) * tileSize, 0);

                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, boardParent);
                tile.name = $"Tile_{x}_{y}";

                bool isLightBrown = (x + y) % 2 == 0;
                tile.GetComponent<SpriteRenderer>().sprite = isLightBrown ? lightBrownTileSprite : darkBrownTileSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
