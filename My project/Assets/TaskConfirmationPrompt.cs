using DG.Tweening;
using UnityEngine;

public class TaskConfirmationPrompt : MonoBehaviour
{
    #region REFERENCES
    public static TaskConfirmationPrompt instance;

    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Transform panel;

    private System.Action _onConfirm;
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
        canvasGroup.alpha = 0f;
        panel.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
    #endregion

    #region TASK CONFIRMATION FUNCTIONS
    public void Show(System.Action onConfirm)
    {
        _onConfirm = onConfirm;

        gameObject.SetActive(true);

        canvasGroup.DOFade(1f, 0.5f);
        panel.DOScale(1f, 0.65f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        canvasGroup.DOFade(0f, 0.5f);
        panel.DOScale(0f, 0.65f).SetEase(Ease.OutBack).OnComplete(() => gameObject.SetActive(false));
    }

    public void Confirm()
    {
        _onConfirm?.Invoke();
        Hide();
    }

    public void Cancel()
    {
        Hide();
    }
    #endregion
}
