using UnityEngine;
using UnityEngine.UI;

public class NewGameTutorial : ActionStack.ActionBehaviour
{
    private bool b_isDone;

    [SerializeField] private Button playButton;

    public override bool IsDone() { return b_isDone; }

    public override void OnEnd() { base.OnEnd(); }

    void Start()
    {
        GameManager.Instance.UIManager.Stack.RemoveAt(0);

        if (GameManager.Instance.IsGameLoaded)
        {
            Destroy(gameObject);
            return;
        }

        GameManager.Instance.UIManager.PushAction(this);
        playButton.onClick.AddListener(ClickPlay);
    }

    private void ClickPlay()
    {
        // Switching input map to gameplay to player be able to perform gameplay actions
        GameManager.Instance.Player.InputManager.SwitchInputMap(InputMap.Gameplay);
        // Initialize player stats
        GameManager.Instance.Player.Stats.NewGameSetupStats();
        // Set Camera follow
        GameManager.Instance.SetCamera();
        Camera.main.GetComponent<DefaultCamera>().enabled = true;

        b_isDone = true;
    }
}