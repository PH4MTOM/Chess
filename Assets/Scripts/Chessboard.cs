using UnityEngine;

public class Chessboard : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform boardParent;
    [SerializeField] private Sprite lightBrownTileSprite;
    [SerializeField] private Sprite darkBrownTileSprite;
    public float tileSize { get; private set; } = 1.28f;

    public void GenerateBoard()
    {
        Debug.Log("Generating Chessboard...");
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Vector2 position = new Vector2((x - 3.5f) * tileSize, (y - 3.5f) * tileSize);

                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, boardParent);
                tile.name = $"Tile_{x}_{y}";

                bool isLightBrown = (x + y) % 2 == 0;
                tile.GetComponent<SpriteRenderer>().sprite = isLightBrown ? lightBrownTileSprite : darkBrownTileSprite;
            }
        }
    }
}
