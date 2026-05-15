using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : ActionStack.ActionBehaviour
{
    private bool b_IsDone;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button upgradesButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    [Header("Panels")]
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject settingsPanel;
    
    private GameObject currentPanel;

    public static event Action OnGameUnpaused;

    public override bool IsDone() { return b_IsDone; }

    public override void OnEnd()
    {
        GameManager.Instance.Player.InputManager.OnUnpause -= UnpauseGame;
        
        base.OnEnd();
        
        // Destroy pause menu gameobject after unpausing
        Destroy(gameObject);
    }

    private void Start()
    {
        // Assigning current panel
        currentPanel = profilePanel;

        GameManager.Instance.UIManager.PushAction(this);

        // Binding buttons on click events to their corresponding functions
        resumeButton.onClick.AddListener(UnpauseGame);
        profileButton.onClick.AddListener(ClickProfile);
        upgradesButton.onClick.AddListener(ClickUpgrades);
        inventoryButton.onClick.AddListener(ClickInventory);
        tutorialButton.onClick.AddListener(ClickTutorial);
        settingsButton.onClick.AddListener(ClickSettings);
        quitButton.onClick.AddListener(ClickQuit);

        GameManager.Instance.Player.InputManager.OnUnpause += UnpauseGame;
    }

    private void ClickProfile() => ClickPanel("profilePanel", ref profilePanel);
    
    private void ClickUpgrades() => ClickPanel("upgradesPanel", ref upgradesPanel);

    private void ClickInventory() => ClickPanel("inventoryPanel", ref inventoryPanel);

    private void ClickTutorial() => ClickPanel("tutorialPanel", ref tutorialPanel);
    
    private void ClickSettings() => ClickPanel("settingsPanel", ref settingsPanel);

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

    // Switch between pause menu panels
    private void ClickPanel(string panelName, ref GameObject nextPanel)
    {
        // Lock for multiple calls on the same panel
        if (currentPanel.name.ToString() == panelName) return;
        
        // Logic for switching panels (deactivating the current panel, activating the panel by corresponding button clicking and setting the activated panel to current
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
        currentPanel = nextPanel;
    }

    // Unpause game
    private void UnpauseGame()
    {
        GameManager.Instance.Player.InputManager.SwitchInputMap(InputMap.Gameplay);
        OnGameUnpaused?.Invoke();
        b_IsDone = true;
    }
}