using System.Collections.Generic;

[System.Serializable]
public class TaskSaveData
{
    public List<string> activeTasks = new List<string>();
    public List<string> completedTasks = new List<string>();
}
