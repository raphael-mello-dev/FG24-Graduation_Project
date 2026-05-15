using UnityEngine;
using UnityEngine.UI;

public class MainMenu : ActionStack.ActionBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject newGameScreen;

    [Header("Buttons groups")]
    [SerializeField] private GameObject mainButtonsGroup;
    [SerializeField] private GameObject playButtonsGroup;

    [Header("Save Window")]
    [SerializeField] private GameObject saveWindow;

    private GameObject canvas;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitButton;

    private bool b_IsDone;

    public override bool IsDone() { return b_IsDone; }

    public override void OnEnd() => base.OnEnd();

    private void Start()
    {
        settingsMenu = Resources.Load<GameObject>("Prefabs/UI/SettingsMenu");
        newGameScreen = Resources.Load<GameObject>("Prefabs/UI/NewGameScreen");

        b_IsDone = false;

        // Canvas reference for parent object 
        canvas = FindFirstObjectByType<Canvas>().gameObject;

        // Adding the main menu into the action stack (UI Manager)
        GameManager.Instance.UIManager.PushAction(this);

        // Binding buttons on click events to their corresponding functions
        playButton.onClick.AddListener(ActiveHideButtonsGroup);
        newGameButton.onClick.AddListener(ClickNewGame);
        continueButton.onClick.AddListener(ClickContinue);
        settingsButton.onClick.AddListener(ClickSettings);
        backButton.onClick.AddListener(ActiveHideButtonsGroup);
        quitButton.onClick.AddListener(ClickQuit);
    }

    public void EndMainMenuAction() => b_IsDone = true;

    // Click to start a new game
    private void ClickNewGame()
    {
        // Instanciating screen for choosing character in the new game mode
        Instantiate(newGameScreen, canvas.transform);

        if (saveWindow.activeInHierarchy) saveWindow.SetActive(false);
    }

    // Click to Continue the saved game
    private void ClickContinue()
    {
        GameManager.Instance.IsGameLoaded = SaveManager.Instance.LoadGame();

        if (GameManager.Instance.IsGameLoaded)
            GameManager.Instance.LoadScene(Scenes.Gameplay);
        else
            saveWindow.SetActive(true);
    }

    // Click to open settings menu
    private void ClickSettings()
    {
        //Create settings menu object on the canvas, overlapping the main menu
        Instantiate(settingsMenu, new Vector3(477, 740, 0), Quaternion.identity, canvas.transform);
    }

    // Click to Quit the game
    private void ClickQuit()
    {
        // Quit the game if you are playing it in Unity
        #if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
        
        // Quit the game of you are playing it in the game application
        #endif
            Application.Quit();
    }

    private void ActiveHideButtonsGroup()
    {
        mainButtonsGroup.SetActive(!mainButtonsGroup.activeSelf);
        playButtonsGroup.SetActive(!playButtonsGroup.activeSelf);
    }
}