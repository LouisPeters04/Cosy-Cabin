using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region GRID MANAGER REFERENCES
    public static GridManager instance;

    [Header("GRID MANAGER SETTINGS")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;

    private Placeable[,,] grid;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
        grid = new Placeable[width, height, 2];
    }
    #endregion

    #region GRID MANAGER FUNCTIONS
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x * cellSize, 0, y * cellSize);
    }

    public void GetGridPosition(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPos.x / cellSize);
        y = Mathf.FloorToInt(worldPos.y / cellSize);
    }

    public bool IsCellFree(int x, int y, int layer)
    {
        if (x < 0 || y < 0 || x>= width || y >= height) return false;

        return grid[x, y, layer];
    }

    public void OccupyCell(int x, int y, int layer, Placeable obj)
    {
        grid[x, y, layer] = obj;
    }
    #endregion
}
