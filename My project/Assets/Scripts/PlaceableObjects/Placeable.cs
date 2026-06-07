using UnityEngine;

public class Placeable : MonoBehaviour
{
    #region PLACEABLE REFERENCES
    [Header("PLACEABLE SETTINGS")]
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    [SerializeField] private bool canStack;
    [SerializeField] private int layer;

    [SerializeField] private int rotationIndex;
    #endregion

    #region PLACEABLE FUNCTIONS
    public void Rotate()
    {
        rotationIndex = (rotationIndex + 1) % 8;
        transform.Rotate(0, 45f, 0);
    }

    public bool CanPlace(int gridX, int gridY, BuildFurniture item)
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                bool free = GridManager.instance.IsCellFree(gridX + x, gridY + y, layer);

                if (!free && !item.canStack)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Place(int gridX, int gridY)
    {
        for (int x = 0; x <= sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                GridManager.instance.OccupyCell(gridX + x, gridY + y, layer, this);
            }
        }
    }
    #endregion
}
