using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    #region SHOP ITEM UI REFERENCES
    [Header("SHOP ITEM UI REFERENCES")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    private ShopItem item;
    #endregion

    #region SHOP ITEM UI FUNCTIONS
    public void Setup(ShopItem newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        nameText.text = item.itemName;
        priceText.text = item.price.ToString();

        bool alreadyOwned = BuildItemGrid.instance.ownedFurniture.Exists(f => f.name == item.furnitureData.name);
        bool alreadyPlaced = BuildSaveManager.instance.placedSave.placed.Exists(f => f.id == item.furnitureData.name);

        buyButton.interactable = !alreadyOwned && !alreadyPlaced;

        buyButton.onClick.AddListener(Buy);
    }

    public void Buy()
    {
        if(CurrencyManager.instance.CabinCoins >= item.price)
        {
            CurrencyManager.instance.AddCoins(-item.price);
            BuildItemGrid.instance.AddItem(item.furnitureData);
        }
        else
        {
            buyButton.image.color = Color.red;
            buyButton.image.DOColor(Color.white, 0.5f);
        }
    }
    #endregion
}
