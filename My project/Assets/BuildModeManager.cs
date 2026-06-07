using UnityEngine;

public class BuildModeManager : MonoBehaviour
{
    #region BUILD MODE MANAGER REFERENCES
    public static BuildModeManager instance;

    [Header("BUILD MODE MANAGER REFERENCES")]
    [SerializeField] private GameObject buildUI;

    public bool BuildModeActive {  get; private set; }
    #endregion

    #region UNITY FUNCTIONS
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region BUILD MODE MANAGER FUNCTIONS
    public void ToggleBuildMode()
    {
        BuildModeActive = !BuildModeActive;

        buildUI.SetActive(BuildModeActive);
        PlacementController.instance.SetBuildMode(BuildModeActive);
    }
    #endregion
}
