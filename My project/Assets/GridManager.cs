using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region GRID MANAGER REFERENCES
    public static GridManager instance;

    [Header("GRID MANAGER SETTINGS")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private MeshRenderer floorRenderer;

    private Placeable[,,] grid;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;

        Vector3 size = floorRenderer.bounds.size;

        width = Mathf.RoundToInt(size.x / cellSize);
        height = Mathf.RoundToInt(size.z / cellSize);

        grid = new Placeable[width, height, 2];
    }
    #endregion

    #region GRID MANAGER FUNCTIONS
    public Vector3 GetWorldPosition(int x, int y)
    {
        Transform t = floorRenderer.transform;

        Vector3 localMin= t.transform.InverseTransformPoint(floorRenderer.bounds.min);

        Vector3 local = new Vector3(x * cellSize, 0, y * cellSize) + localMin;
        return t.TransformPoint(local);
    }

    public void GetGridPosition(Vector3 worldPos, out int x, out int y)
    {
        Transform t = floorRenderer.transform;

        Vector3 local = t.transform.InverseTransformPoint(worldPos);

        Vector3 minLocal = t.InverseTransformPoint(floorRenderer.bounds.min);

        local -= minLocal;

        x = Mathf.FloorToInt(local.x / cellSize);
        y = Mathf.FloorToInt(local.z / cellSize);
    }

    public bool IsCellFree(int x, int y, int layer)
    {
        if (x < 0 || y < 0 || x >= width || y >= height) return false;

        return grid[x, y, layer] == null;
    }

    public void OccupyCell(int x, int y, int layer, Placeable obj)
    {
        grid[x, y, layer] = obj;
    }

    public void ClearCell(int x, int y, int layer)
    {
        grid[x, y, layer] = null;
    }

    #endregion
}
