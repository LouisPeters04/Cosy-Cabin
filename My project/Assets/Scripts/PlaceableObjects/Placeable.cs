using UnityEngine;

public class Placeable : MonoBehaviour
{
    #region PLACEABLE REFERENCES
    [Header("PLACEABLE SETTINGS")]
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    [SerializeField] private bool canStack;
    [SerializeField] private int layer;

    [SerializeField] public int rotationIndex;
    [SerializeField] public float heightOffset;
    [SerializeField] public BuildFurniture furnitureData;

    public int placedX;
    public int placedY;

    #endregion

    #region PLACEABLE FUNCTIONS
    public void Rotate()
    {
        rotationIndex = (rotationIndex + 1) % 8;
        transform.Rotate(0, 45f, 0);

        if (rotationIndex % 2 == 1)
        {
            int temp = sizeX;
            sizeX = sizeY;
            sizeY = temp;
        }
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
        placedX = gridX;
        placedY = gridY;
        for (int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                GridManager.instance.OccupyCell(gridX + x, gridY + y, layer, this);
            }
        }

        Vector3 pos = transform.position;
        pos.y = heightOffset;
        transform.position = pos;
    }

    public void ClearCells()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                GridManager.instance.ClearCell(placedX + x, placedY + y, layer);
            }
        }
    }

    #endregion
}
