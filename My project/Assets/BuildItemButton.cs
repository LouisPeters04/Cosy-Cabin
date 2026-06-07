using UnityEngine;
using UnityEngine.UI;

public class BuildItemButton : MonoBehaviour
{
    public BuildFurniture furniture;
    public Button button;
    public Image icon;

    private void Awake()
    {
        button = GetComponent<Button>();
        icon = GetComponent<Image>();
    }
}
