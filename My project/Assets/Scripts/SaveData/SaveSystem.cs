using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string Path => Application.persistentDataPath + "/tasks.json";

    public static void Save(TaskSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Path, json);
    }

    public static TaskSaveData Load()
    {
        if (!File.Exists(Path))
        {
            return new TaskSaveData();
        }

        string json = File.ReadAllText(Path);
        return JsonUtility.FromJson<TaskSaveData>(json);
    }
}
