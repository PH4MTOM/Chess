using UnityEngine;

public class Indicator : MonoBehaviour
{
    public (int, int) CurrentTilePosition { get; private set; }

    public void Init((int, int) tilePosition)
    {
        CurrentTilePosition = tilePosition;
    }
}
