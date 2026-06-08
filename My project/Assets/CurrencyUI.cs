using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    #region CURRENCY UI REFERENCES
    public static CurrencyUI instance;

    [Header("CURRENCY UI REFERENCES")]
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text popupText; 
    [SerializeField] private CanvasGroup popupGroup;
    #endregion
    private void Awake()
    {
        instance = this;
        popupGroup.alpha = 0f;
    }

    public void UpdateCoins(int amount)
    {
        coinText.text = amount.ToString();
    }

    public void ShowPopup(int amount)
    {
        popupText.text = $"+{amount}";
        popupGroup.alpha = 0f;

        popupText.transform.localScale = Vector3.one * 0.5f;

        popupGroup.DOFade(1f, 0.15f);
        popupText.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);

        popupGroup.DOFade(0f, 0.3f).SetDelay(0.6f);
    }
}
