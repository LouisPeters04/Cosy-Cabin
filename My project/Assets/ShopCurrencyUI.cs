using UnityEngine;
using TMPro;

public class ShopCurrencyUI : MonoBehaviour
{
    #region SHOP CURRENCY UI REFERENCES
    public static ShopCurrencyUI instance;

    [Header("SHOP CURRENCY UI REFERENCES")]
    [SerializeField] private TMP_Text currencyText;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region SHOP CURRENCY UI FUNCTIONS
    public void UpdateCurrency(int amount)
    {
        currencyText.text = amount.ToString();
    }
    #endregion
}
