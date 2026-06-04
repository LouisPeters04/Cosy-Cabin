using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    #region REFERENCES
    [Header("UI REFERENCES")]

    [SerializeField] private TMP_InputField taskInput;
    [SerializeField] private Button addTaskButton;

    [SerializeField] private GameObject taskPanel;
    [SerializeField] private Image buttonImage;

    [Header("CONTENT TRANSFORMS")]
    [SerializeField] private Transform activeTaskContent;
    [SerializeField] private Transform completedTaskContent;

    [Header("PREFAB REFERENCES")]
    [SerializeField] private GameObject taskPrefab;

    private TaskSaveData _saveData;

    #endregion

    #region UNITY FUNCTIONS

    private void Start()
    {
        _saveData = SaveSystem.Load();

        addTaskButton.onClick.AddListener(AddTask);
        taskInput.onEndEdit.AddListener(OnInputSubmit);

        LoadActiveTasks();
        LoadCompletedTasks();
    }
    #endregion

    #region TASK MANAGER FUNCTIONS

    private void AddTask()
    {
        if (_saveData.activeTasks.Count >= 5)
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(taskInput.text))
        {
            return;
        }

        string text = taskInput.text;
        _saveData.activeTasks.Add(text);

        CreateTaskUI(text, false, activeTaskContent);
        taskPanel.SetActive(false);

        taskInput.text = "";
        SaveSystem.Save(_saveData);
    }

    private void LoadActiveTasks()
    {
        foreach (string task in _saveData.activeTasks)
        {
            CreateTaskUI(task, false, activeTaskContent);
        }
    }

    private void LoadCompletedTasks()
    {
        foreach (string task in _saveData.completedTasks)
        {
            CreateTaskUI(task, true, completedTaskContent);
        }
    }

    private void CreateTaskUI(string text, bool completed, Transform parent)
    {
        GameObject obj = Instantiate(taskPrefab, parent);
        TaskItem item = obj.GetComponent<TaskItem>();
        item.Init(text, completed, OnTaskCompleted, OnTaskDiscarded);
    }

    private void OnTaskCompleted(TaskItem item)
    {
        string text = item.taskText.text;

        _saveData.activeTasks.Remove(text);
        _saveData.completedTasks.Add(text);

        item.transform.SetParent(completedTaskContent);
        item.transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutCubic);

        SaveSystem.Save(_saveData);
    }

    private void OnTaskDiscarded(TaskItem item)
    {
        string text = item.taskText.text;

        _saveData.activeTasks.Remove(text);
        _saveData.completedTasks.Remove(text);

        SaveSystem.Save( _saveData);
    }

    private void OnInputSubmit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            AddTask();
        }
    }

    public void ShowTaskPanel()
    {
        if (_saveData.activeTasks.Count >= 5)
        {
            buttonImage.color = Color.red;
            buttonImage.DOColor(Color.white, 0.5f);
            return;
        }

        taskPanel.SetActive(true);

        taskPanel.transform.localScale = new Vector3(1,1,1);
        taskPanel.transform.DOScaleY(1f, 0.25f).SetEase(Ease.OutCubic);

        taskInput.Select();
        taskInput.ActivateInputField();
    }
    #endregion
}
