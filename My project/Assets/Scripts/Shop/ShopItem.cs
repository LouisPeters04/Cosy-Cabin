using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Objects/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public BuildFurniture furnitureData;
}
