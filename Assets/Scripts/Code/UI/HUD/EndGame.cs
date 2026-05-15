using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : ActionStack.ActionBehaviour
{
    private bool b_IsDone;
    
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameWonScreen;
    [SerializeField] private Button menuButton;

    public override bool IsDone() { return b_IsDone; }

    void Start()
    {
        GameManager.Instance.UIManager.Stack.RemoveAt(0);
        GameManager.Instance.UIManager.PushAction(this);

        menuButton.onClick.AddListener(BackToMenu);

        if (GameManager.Instance.Player.Stats.CurrentHealth == 0)
        {
            gameOverScreen.SetActive(true);
            StartCoroutine("End");
        }
        else
            gameWonScreen.SetActive(true);
        
    }

    private IEnumerator End()
    {
        yield return new WaitForSecondsRealtime(7f);
        BackToMenu();
    }

    private void BackToMenu()
    {
        b_IsDone = true;
        GameManager.Instance.LoadScene(Scenes.MainMenu);
        GameManager.Instance.ResetPlayer();
        GameManager.Instance.ResetCamera();
    }
}