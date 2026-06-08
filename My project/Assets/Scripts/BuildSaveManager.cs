using UnityEngine;

public class BuildSaveManager : MonoBehaviour
{
    public static BuildSaveManager instance;

    public PlacedFurnitureSave placedSave;

    private void Awake()
    {
        instance = this;

        placedSave = SaveSystem.LoadPlacedFurniture();
        if (placedSave == null)
            placedSave = new PlacedFurnitureSave();

        LoadPlacedObjects();
    }

    private void LoadPlacedObjects()
    {
        foreach (var data in placedSave.placed)
        {
            BuildFurniture furniture = BuildItemGrid.instance.allFurniture.Find(f => f.name == data.id);
            if (furniture == null) continue;

            BuildItemGrid.instance.ownedFurniture.Remove(furniture);

            var obj = Instantiate(furniture.prefab);

            obj.transform.position = GridManager.instance.GetWorldPosition(data.x, data.y);
            obj.transform.rotation = Quaternion.Euler(0, data.rotation, 0);

            var placed = obj.AddComponent<PlacedFurniture>();
            placed.furnitureData = furniture;
            placed.x = data.x;
            placed.y = data.y;
            placed.rotation = data.rotation;

            obj.GetComponent<Placeable>().Place(data.x, data.y);
        }
    }
    public void AddPlacedFurniture(BuildFurniture furniture, int x, int y, int rotation)
    {
        placedSave.placed.Add(new PlacedFurnitureData
        {
            id = furniture.name,
            x = x,
            y = y,
            rotation = rotation
        });

        SaveSystem.SavePlacedFurniture(placedSave);
    }

    public void RemovePlacedFurniture(BuildFurniture furniture, int x, int y)
    {
        placedSave.placed.RemoveAll(p => p.id == furniture.name && p.x == x && p.y == y);
        SaveSystem.SavePlacedFurniture(placedSave);
    }

}
