using UnityEngine;

public class BuildModeManager : MonoBehaviour
{
    #region BUILD MODE MANAGER REFERENCES
    public static BuildModeManager instance;

    [Header("BUILD MODE MANAGER REFERENCES")]
    [SerializeField] private GameObject buildUI;
    [SerializeField] private BuildPanelAnimator animator;
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

        if (BuildModeActive)
        {
            animator.Show();
        }
        else
        {
            animator.Hide();
        }
        PlacementController.instance.SetBuildMode(BuildModeActive);
    }
    #endregion
}
