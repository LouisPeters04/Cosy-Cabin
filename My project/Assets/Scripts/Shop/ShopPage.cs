using UnityEngine;

[CreateAssetMenu(fileName = "ShopPage", menuName = "Scriptable Objects/ShopPage")]
public class ShopPage : ScriptableObject
{
    public string pageName;
    public ShopItem[] items;
}
