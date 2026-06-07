using UnityEngine;
using DG.Tweening;

public class BuildPanelAnimator : MonoBehaviour
{
    #region BUILD PANEL ANIMATOR REFERENCES
    [Header("BUILD PANEL REFERENCES")]
    [SerializeField] private RectTransform panel;
    [SerializeField] private float duration;
    #endregion
    #region UNITY FUNCTIONS
    private void Awake()
    {
        panel.localScale = new Vector3 (0f, 1f, 1f);
    }
    #endregion
    #region BUILD PANEL ANIMATOR FUNCTIONS
    public void Show()
    {
        panel.DOScaleX(1f, duration).SetEase(Ease.OutCubic);
    }

    public void Hide()
    {
        panel.DOScaleX(0f, duration).SetEase(Ease.InCubic);
    }
    #endregion
}
