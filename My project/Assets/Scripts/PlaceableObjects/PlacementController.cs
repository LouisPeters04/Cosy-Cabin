using UnityEngine;

public class PlacementController : MonoBehaviour
{
    #region PLACEMENT CONTROLLER REFERENCES
    public static PlacementController instance;

    [Header("PLACEMENT CONTROLLER REFERENCES")]
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject previewPrefab;

    private PlacementPreview preview;
    private BuildFurniture selectedItem;
    private bool _buildMode;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!_buildMode || selectedItem == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GridManager.instance.GetGridPosition(hit.point, out int x, out int y);

            preview.UpdatePreview(x, y);

            if (Input.GetKeyDown(KeyCode.R))
            {
                preview.Rotate();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                preview.MoveForward();
            }

            if (Input.GetMouseButton(0))
            {
                TryPlace(x, y);
            }
        }
    }
    #endregion

    #region PLACEMENT CONTROLLER REFERENCES
    public void SetBuildMode(bool active)
    {
        _buildMode = active;

        if(!active && preview != null)
        {
            Destroy(preview.gameObject);
        }
    }
    public void SelectItem(BuildFurniture item)
    {
        selectedItem = item;

        if (preview != null)
        {
            Destroy(preview.gameObject);
        }

        preview = Instantiate(item.prefab).AddComponent<PlacementPreview>();

        preview.Init(item);
    }

    private void TryPlace(int x, int y)
    {
        if (preview.CanPlace(x, y))
        {
            var obj = Instantiate(selectedItem.prefab);
            obj.transform.position = preview.transform.position;

            obj.GetComponent<Placeable>().Place(x, y);
        }
    }
    #endregion
}
