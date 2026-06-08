using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class BuildItemGrid : MonoBehaviour
{
    #region BUILD ITEM GRID REFERENCES
    public static BuildItemGrid instance;

    [Header("BUILD ITEM GRID REFERENCES")]
    [SerializeField] private Transform content;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] public List<BuildFurniture> allFurniture;
    [SerializeField] public List<BuildFurniture> ownedFurniture = new List<BuildFurniture>();

    private OwnedFurnitureSaveData _saveData;
    private ItemSize _currentCategory;
    #endregion
    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
        LoadOwnedFurniture();

        if(_saveData == null)
        {
            _saveData = new OwnedFurnitureSaveData();
        }

        ShowCategory(_currentCategory);
    }
    #endregion
    #region BUILD ITEM GRID FUNCTIONS
    public void ShowCategory(ItemSize size)
    {
        _currentCategory = size;

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in ownedFurniture)
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

    public void AddItem(BuildFurniture item)
    {
        if(!ownedFurniture.Contains(item))
        {
            ownedFurniture.Add(item);
        }

        SaveOwnedItems();

        if (item.size == _currentCategory)
        {
            var buttonObj = Instantiate(itemButtonPrefab, content);
            var itemButton = buttonObj.GetComponent<BuildItemButton>();

            itemButton.furniture = item;
            itemButton.icon.sprite = item.Icon;

            itemButton.button.onClick.AddListener(() =>
            {
                PlacementController.instance.SelectItem(item);
            });

            buttonObj.transform.localScale = Vector3.zero;
            buttonObj.transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
        }
    }

    private void LoadOwnedFurniture()
    {
        _saveData = SaveSystem.LoadOwnedFurniture();

        if(_saveData == null)
        {
            _saveData = new OwnedFurnitureSaveData();
            return;
        }

        ownedFurniture.Clear();

        foreach (string id in _saveData.ownedFurnitureIDs)
        {
            var item = allFurniture.Find(f => f.name == id);
            if (item != null)
            {
                ownedFurniture.Add(item);
            }
        }
    }

    public void SaveOwnedItems()
    {
        _saveData.ownedFurnitureIDs.Clear();

        foreach(var item in ownedFurniture)
        {
            _saveData.ownedFurnitureIDs.Add(item.name);
        }

        SaveSystem.SaveOwnedFurniture(_saveData);
    }


    #endregion
}
