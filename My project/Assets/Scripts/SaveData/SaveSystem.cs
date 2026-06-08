using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string Path => Application.persistentDataPath + "/tasks.json";

    private static string currencyPath => Application.persistentDataPath + "/currency.json";

    private static string furniturePath => Application.persistentDataPath + "/furniture.json";

    private static string placedPath => Application.persistentDataPath + "/placedFurniture.json";

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

    public static void SaveCurrency(CurrencySaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(currencyPath, json);
    }

    public static CurrencySaveData LoadCurrency()
    {
        if (!File.Exists(currencyPath))
        {
            return null;
        }

        string json = File.ReadAllText(currencyPath);
        return JsonUtility.FromJson<CurrencySaveData>(json);
    }

    public static void SaveOwnedFurniture(OwnedFurnitureSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(furniturePath, json);
    }

    public static OwnedFurnitureSaveData LoadOwnedFurniture()
    {
        if (!File.Exists(furniturePath))
        {
            return null;
        }

        string json = File.ReadAllText(furniturePath);
        return JsonUtility.FromJson<OwnedFurnitureSaveData>(json);
    }

    public static void SavePlacedFurniture (PlacedFurnitureSave data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(placedPath, json);
    }

    public static PlacedFurnitureSave LoadPlacedFurniture()
    {
        if (!File.Exists(placedPath))
        {
            return null;
        }

        string json = File.ReadAllText(placedPath);
        return JsonUtility.FromJson<PlacedFurnitureSave>(json);
    }
}


