using UnityEngine;
using UnityEngine.UI;

public enum ItemSize { Large, Medium, Small}

[CreateAssetMenu(fileName = "BuildFurniture", menuName = "Scriptable Objects/BuildFurniture")]
public class BuildFurniture : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
    public GameObject prefab;
    public ItemSize size;

    public bool canStack;
    public bool placeAbove;
    public bool placeBelow;
}
