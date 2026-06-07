using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildItemGrid : MonoBehaviour
{
    #region BUILD ITEM GRID REFERENCES
    public static BuildItemGrid instance;

    [Header("BUILD ITEM GRID REFERENCES")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private List<BuildFurniture> furnitureList;
    #endregion
    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
    }
    #endregion
    #region BUILD ITEM GRID FUNCTIONS
    public void ShowCategory(ItemSize size)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in furnitureList)
        {
            if (item.size != size) continue;

            var buttonObj = Instantiate(itemButtonPrefab, content);
            var itemButton = buttonObj.GetComponent<BuildItemButton>();

            itemButton.furniture = item;
            itemButton.icon.sprite = item.Icon;

            itemButton.button.onClick.AddListener(() =>
            {
                PlacementController.instance.SelectItem(item);
            });
        }
    }

    public void ShowCategoryByString(string sizeName)
    {
        if (System.Enum.TryParse(sizeName, true, out ItemSize parsedSize))
        {
            ShowCategory(parsedSize);
        }
        else
        {

        }
    }
    public void RemoveItem(BuildFurniture item)
    {
        foreach (Transform child in content)
        {
            var btn = child.GetComponent<BuildItemButton>();
            if (btn != null && btn.furniture == item)
            {
                btn.gameObject.SetActive(false);
                return;
            }
        }
    }
    public void RestoreItem(BuildFurniture item)
    {
        foreach (Transform child in content)
        {
            var btn = child.GetComponent<BuildItemButton>();
            if (btn != null && btn.furniture == item)
            {
                btn.gameObject.SetActive(true);
                return;
            }
        }
    }


    #endregion
}
