using UnityEngine.SceneManagement;
using UnityEngine;

// Enum containing all loadable scenes in the game
public enum Scenes
{
    MainMenu = 0,
    Gameplay = 1
}

public class GameManager : MonoBehaviour
{
    // Game Manager singleton
    public static GameManager Instance { get; private set; }

    #region Managers

    //Log Manager instance
    public LogManager LogManager { get; private set; }

    // Action stack instance to control UI flow
    private ActionStack uiManager;
    public ActionStack UIManager { get { return uiManager; } }

    // Action stack to manage camera actions
    private CameraController cameraManager;
    public CameraController CameraManager { get { return cameraManager; } }

    #endregion

    public bool IsGameLoaded = false;

    // Player
    private PlayerController player;
    public PlayerController Player { get { return player; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LogManager = new LogManager();
        LogManager.CanLogMessage = true;
        uiManager = new ActionStack();
        
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        UIManager.UpdateActions();
        
        if (player != null) Player.UpdateActions();
        if (cameraManager != null) CameraManager.UpdateActions();
    }

    // Function for loading desired scene
    public void LoadScene(Scenes scene) => SceneManager.LoadScene((int) scene);

    // Setting and reseting player and camera instances
    public void SetPlayer() => player = new PlayerController();
    public void SetCamera() => cameraManager = new CameraController();
    public void ResetPlayer() => player = null; 
    public void ResetCamera() => cameraManager = null;

    // Function for displaying in the screen the list of actions and its current priority
    private void OnGUI()
    {
        if (UIManager == null) return;

        #if UNITY_EDITOR

        const float LINE_HEIGHT = 32f;

        GUI.color = new Color(0, 0, 0, 0.7f);
        Rect stackRect = new Rect(0, 0, 250f, LINE_HEIGHT * UIManager.Stack.Count);
        GUI.DrawTexture(stackRect, Texture2D.whiteTexture);

        Rect actionLine = new Rect(10f, 0, stackRect.width - 20f, LINE_HEIGHT);

        for (int i = 0; i < UIManager.Stack.Count; ++i)
        {
            GUI.color = UIManager.Stack[i] == UIManager.CurrentAction ? Color.green : Color.white;
            GUI.Label(actionLine, $"#{i}: {UIManager.Stack[i].ToString()}");
            actionLine.y += actionLine.height;
        }

        #endif

        //if (player == null) return;

        //#if UNITY_EDITOR

        //const float LINE_HEIGHT = 32f;

        //GUI.color = new Color(0, 0, 0, 0.7f);
        //Rect stackRect = new Rect(0, 0, 250f, LINE_HEIGHT * player.Stack.Count);
        //GUI.DrawTexture(stackRect, Texture2D.whiteTexture);

        //Rect actionLine = new Rect(10f, 0, stackRect.width - 20f, LINE_HEIGHT);

        //for (int i = 0; i < player.Stack.Count; ++i)
        //{
        //    GUI.color = player.Stack[i] == player.CurrentAction ? Color.green : Color.white;
        //    GUI.Label(actionLine, $"#{i}: {player.Stack[i].ToString()}");
        //    actionLine.y += actionLine.height;
        //}

        //#endif
    }
}