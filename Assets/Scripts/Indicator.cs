using UnityEngine;

public class Indicator : MonoBehaviour
{
    public (int, int) CurrentTilePosition;

    public void Init((int, int) tilePosition)
    {
        CurrentTilePosition = tilePosition;
    }
}
