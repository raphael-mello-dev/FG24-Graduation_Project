using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private TextMeshProUGUI gameLevelText;
    [SerializeField] private Slider angleSlider;
    [SerializeField] private Slider distanceSlider;

    [Header("Graphics")]
    [SerializeField] private TMP_Dropdown screenModeDropdown;
    [SerializeField] private TMP_Dropdown screenResolutionDropdown;
    [SerializeField] private TMP_Dropdown graphicsQualityDropdown;
    [SerializeField] private Toggle vsynchToggle;

    //[Header("Audio")]

    private void Start()
    {
        gameLevelText.text += GameSettings.Instance.Difficulty.ToString();
        angleSlider.onValueChanged.AddListener(GameManager.Instance.CameraManager.SetFollowAngle);
        distanceSlider.onValueChanged.AddListener(GameManager.Instance.CameraManager.SetLookDistance);
        screenModeDropdown.onValueChanged.AddListener(SetScreenMode);
        screenResolutionDropdown.onValueChanged.AddListener(SetScreenResolution);
        graphicsQualityDropdown.onValueChanged.AddListener(SetGraphicsQuality);
        vsynchToggle.onValueChanged.AddListener(SetVSynch);
    }

    private void OnDestroy()
    {
        angleSlider.onValueChanged.RemoveAllListeners();
        distanceSlider.onValueChanged.RemoveAllListeners();
        screenModeDropdown.onValueChanged.RemoveAllListeners();
        screenResolutionDropdown.onValueChanged.RemoveAllListeners();
        graphicsQualityDropdown.onValueChanged.RemoveAllListeners();
        vsynchToggle.onValueChanged.RemoveAllListeners();

        screenResolutionDropdown.interactable = false;
    }

    private void SetScreenMode(int index)
    {
        switch (index)
        {
            case 0:
                GameSettings.Instance.Fullscreen = FullScreenMode.FullScreenWindow;
                screenResolutionDropdown.interactable = false;
            break;

            case 1:
                GameSettings.Instance.Fullscreen = FullScreenMode.MaximizedWindow;
                screenResolutionDropdown.interactable = false;
            break;

            case 2:
                GameSettings.Instance.Fullscreen = FullScreenMode.Windowed;
                screenResolutionDropdown.interactable = true;
            break;
        }

        Screen.fullScreenMode = GameSettings.Instance.Fullscreen;
    }

    private void SetScreenResolution(int index)
    {
        Screen.SetResolution(GameSettings.Instance.Resolutions[index, 0], GameSettings.Instance.Resolutions[index, 1], false);
    }

    private void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    private void SetVSynch(bool value)
    {
        GameSettings.Instance.VSynch = (value) ? Switch.On : Switch.Off;
    }
}