using UnityEngine;
using DG.Tweening;

public class ShopWindow : MonoBehaviour
{
    #region SHOP WINDOW REFERENCES
    [Header("SHOP WINDOW REFERENCES")]
    [SerializeField] private RectTransform shopPanel;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        shopPanel.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
    #endregion

    #region SHOP WINDOW FUNCTIONS
    public void OpenShop()
    {
        gameObject.SetActive(true);

        ShopCurrencyUI.instance.UpdateCurrency(CurrencyManager.instance.CabinCoins);

        shopPanel.localScale = Vector3.zero;
        shopPanel.DOScale(1f, 0.35f).SetEase(Ease.OutBack);
    }

    public void CloseShop()
    {
        shopPanel.DOScale(0f, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    #endregion
}
