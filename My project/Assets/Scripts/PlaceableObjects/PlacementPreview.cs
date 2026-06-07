using UnityEngine;

public class PlacementPreview : MonoBehaviour
{
    #region PLACEMENT PREVIEW REFERENCES
    private BuildFurniture item;
    public Placeable placeable;
    private MeshRenderer[] renderers;
    #endregion

    #region PLACEMENT PREVIEW FUNCTIONS
    public void Init(BuildFurniture item)
    {
        this.item = item;
        placeable = GetComponent<Placeable>();
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void UpdatePreview(int x, int y)
    {
        bool valid = placeable.CanPlace(x, y, item);

        foreach (var r in renderers)
        {
            r.material.color = valid? Color.green : Color.red;
        }

        transform.position = GridManager.instance.GetWorldPosition(x, y);
    }

    public void Rotate()
    {
        placeable.Rotate();
    }

    public void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * 2f;
    }

    public bool CanPlace(int x, int y)
    {
        return placeable.CanPlace(x, y, item);
    }
    #endregion
}
