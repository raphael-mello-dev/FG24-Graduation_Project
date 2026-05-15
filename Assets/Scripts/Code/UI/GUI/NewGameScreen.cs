using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGameScreen : ActionStack.ActionBehaviour
{
    private bool b_IsDone;

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TextMeshProUGUI charactersText;
    [SerializeField] private TMP_Dropdown levelDifficultyDropdown;
    
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button backButton;

    private string[] characters = { "Paladin" }; //{ "Paladin", "Male", "Female" };
    private Int32 characterIndex;

    void Start()
    {
        // Adding the new game screen into the action stack (UI Manager)
        GameManager.Instance.UIManager.PushAction(this);

        // Setting Character selection
        characterIndex = 0;
        charactersText.text = characters[characterIndex];

        // Binding buttons on click events to their corresponding functions
        leftButton.onClick.AddListener(ClickLeft);
        rightButton.onClick.AddListener(ClickRight);
        startButton.onClick.AddListener(ClickStart);
        backButton.onClick.AddListener(ClickBack);
    }

    public override bool IsDone() { return b_IsDone; }

    public override void OnEnd() => base.OnEnd();

    // function for swiping left on character selection
    private void ClickLeft()
    {
        if (characterIndex == 0)
        {
            characterIndex = characters.Length - 1;
            charactersText.text = characters[characterIndex];
            return;
        }

        characterIndex--;
        charactersText.text = characters[characterIndex];
    }

    // function for swiping right on character selection
    private void ClickRight()
    {
        if (characterIndex == characters.Length - 1)
        {
            characterIndex = 0;
            charactersText.text = characters[characterIndex];
            return;
        }

        characterIndex++;
        charactersText.text = characters[characterIndex];
    }

    private void ClickStart()
    {
        // Safety lock in case the player doesn't choose the player's name
        string cn = nameInputField.text.ToLower();
        cn = Regex.Replace(cn, @"\s", string.Empty);

        if (string.IsNullOrEmpty(cn) || cn.Contains("character")) return;

        // New game screen action is done, trigger OnEnd
        b_IsDone = true;

        GameSettings.Instance.Difficulty = (GameDifficulty) levelDifficultyDropdown.value;
        SaveManager.Instance.CreateGameData();

        // Setting player
        GameManager.Instance.SetPlayer();
        GameManager.Instance.Player.SetPlayer(nameInputField.text, $"Prefabs/Player/{characters[characterIndex]}_Player");

        // Load Game Scene
        GameManager.Instance.LoadScene(Scenes.Gameplay);
    }

    private void ClickBack()
    {
        // New game screen action is done, trigger OnEnd
        b_IsDone = true;

        // Destroy new game screen object
        Destroy(gameObject);
    }
}