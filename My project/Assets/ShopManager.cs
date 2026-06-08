using System;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region SHOP MANAGER REFERENCES
    public static ShopManager Instance;

    [Header("SHOP MANAGER REFERENCES")]
    [SerializeField] private ShopPage[] pages;
    [SerializeField] private Transform itemGrid;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private TMP_Text pageNumberText;

    private int _currentPage;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowPage(0);
    }
    #endregion

    #region SHOP MANAGER FUNCTIONS
    public void ShowPage(int index)
    {
        _currentPage = index;

        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }
        
        var page = pages[index];

        foreach (var item in pages[index].items)
        {
            var obj = Instantiate(itemPrefab, itemGrid);
            var ui = obj.GetComponent<ShopItemUI>();
            ui.Setup(item);
        }

        pageNumberText.text = $"Page {index + 1} / {pages.Length}";
    }
    public void NextPage()
    {
        _currentPage++;
        if(_currentPage >= pages.Length)
        {
            _currentPage = 0;
        }

        ShowPage(_currentPage);
    }

    public void PreviousPage()
    {
        _currentPage--;
        if (_currentPage < 0)
        {
            _currentPage = pages.Length - 1;
        }
        
        ShowPage(_currentPage);
    }
    #endregion
}
