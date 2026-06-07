using DG.Tweening;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    #region PLACEMENT CONTROLLER REFERENCES
    public static PlacementController instance;

    [Header("PLACEMENT CONTROLLER REFERENCES")]
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject previewPrefab;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private LayerMask placeableMask;
    [SerializeField] private BuildPanelAnimator buildPanelAnimator;

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
        if (!_buildMode) return;

        if (Input.GetMouseButtonDown(1))
        {
            Ray pickRay = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(pickRay, out RaycastHit pickHit, 100f, placeableMask))
            {
                Placeable p = pickHit.collider.GetComponent<Placeable>();
               
                if (p == null) return;

                BuildItemGrid.instance.RestoreItem(p.furnitureData);

                p.ClearCells();

                selectedItem = p.furnitureData;
               
                TweenUtils.PopScale(p.transform);
                Destroy(p.gameObject, 0.15f);

                if (preview != null)
                {
                    Destroy(preview.gameObject);
                }

                preview = Instantiate(selectedItem.prefab).AddComponent<PlacementPreview>();
                preview.Init(selectedItem);
                TweenUtils.PopScale(p.transform);
                return;
            }
        }

        if (selectedItem == null || preview == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, floorMask))
        {
            GridManager.instance.GetGridPosition(hit.point, out int x, out int y);

            preview.UpdatePreview(x, y);

            Vector3 pos = preview.transform.position;
            pos.y = hit.point.y + preview.placeable.heightOffset;
            preview.transform.position = pos;

            if (Input.GetKeyDown(KeyCode.R))
            {
                preview.Rotate();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                preview.MoveForward();
            }

            if (Input.GetMouseButtonDown(0))
            {
                TryPlace(x, y);
            }
        }

        if (selectedItem != null && preview != null && Input.GetKeyDown(KeyCode.Q))
        {
            BuildItemGrid.instance.RestoreItem(selectedItem);
            preview.transform.DOScale(0f, 0.15f).SetEase(Ease.InBack);

            Destroy(preview.gameObject, 0.15f);

            preview = null;

            selectedItem = null;
            return;
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

        if (active)
        {
            buildPanelAnimator.Show();
        }
        else
        {
            buildPanelAnimator.Hide();
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
        if (preview == null || selectedItem == null) return;

        if (preview.CanPlace(x, y))
        {
            var obj = Instantiate(selectedItem.prefab);
            obj.transform.position = preview.transform.position;
            obj.transform.rotation = preview.transform.rotation;

            obj.GetComponent<Placeable>().Place(x, y);

            TweenUtils.PopScale(obj.transform);

            Destroy(preview.gameObject);
            preview = null;

            BuildItemGrid.instance.RemoveItem(selectedItem);
            selectedItem = null;
        }
    }
    #endregion
}
