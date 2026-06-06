using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    #region REFERENCES
    [Header("TASK ITEM REFERENCES")]
    [SerializeField] private Button completeTaskButton;
    [SerializeField] private Button discardTaskButton;

    [SerializeField] public TMP_Text taskText;

    private System.Action<TaskItem> onComplete;
    private System.Action<TaskItem> onDiscard;
    #endregion

    #region TASK ITEM FUNCTIONS
    public void Init(string text, bool completed, System.Action<TaskItem> onCompleteCallBack, System.Action<TaskItem> onDiscardCallBack)
    {
        taskText.text = text;
        onComplete = onCompleteCallBack;
        onDiscard = onDiscardCallBack;

        completeTaskButton.onClick.AddListener(CompleteTask);
        discardTaskButton.onClick.AddListener(OnDiscardPressed);

        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack);

        if (completed)
        {
            ConvertToCompletedVisual();
        }
    }

    private void CompleteTask()
    {
        transform.DOPunchScale(Vector3.one * 0.15f, 0.2f, 8, 1);

        ConvertToCompletedVisual();
        onComplete?.Invoke(this);
    }

    private void DiscardTask()
    {
        transform.DOScale(0f, 0.2f).OnComplete(() =>
        {
            onDiscard?.Invoke(this);
            Destroy(gameObject);
        });
    }

    private void ConvertToCompletedVisual()
    {
        taskText.color = Color.gray;

        completeTaskButton.gameObject.SetActive(false);
        discardTaskButton.gameObject.SetActive(false);
    }

    public void OnDiscardPressed()
    {
        TaskConfirmationPrompt.instance.Show(() =>
        {
            DiscardTask();
        });
    }
    #endregion
}
