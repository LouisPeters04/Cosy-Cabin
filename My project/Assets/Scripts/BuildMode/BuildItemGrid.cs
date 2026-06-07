using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildItemGrid : MonoBehaviour
{
    #region BUILD ITEM GRID REFERENCES
    [Header("BUILD ITEM GRID REFERENCES")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private List<BuildFurniture> furnitureList;
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

            var button = Instantiate(itemButtonPrefab, content);
            button.GetComponent<Image>().sprite = item.Icon;

            button.GetComponent<Button>().onClick.AddListener(() =>
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
    #endregion
}
