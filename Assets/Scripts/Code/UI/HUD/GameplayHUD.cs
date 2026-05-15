using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameplayHUD : ActionStack.ActionBehaviour
{
    private bool b_IsDone;

    private GameObject canvas;
    private GameObject pauseMenu;
    private GameObject endGameScreen;

    [Header("Objects")]
    [SerializeField] private GameObject barsGroup;
    [SerializeField] private GameObject missionObj;
    [SerializeField] private GameObject levelUpObj;

    [Header("Bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image staminaBar;

    private GameObject[] enemies;
    private int enemiesDefeated, enemiesTotal;

    [Header("Text labels")]
    [SerializeField] private TextMeshProUGUI enemiesDefeatedText;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private TextMeshProUGUI levelUpTextOverlay;

    public override bool IsDone() { return b_IsDone; }

    public override void OnEnd() => base.OnEnd();

    void Start()
    {
        // Add Gameplay HUD to the stack
        GameManager.Instance.UIManager.PushAction(this);
        
        // Assigning canvas, pause menu and end game screen objects
        canvas = transform.parent.gameObject;
        pauseMenu = Resources.Load<GameObject>("Prefabs/UI/PauseMenu");
        endGameScreen = Resources.Load<GameObject>("Prefabs/UI/EndGame");

        // Binding functions to respective events
        GameManager.Instance.Player.InputManager.OnPause += PauseGame;
        PauseMenu.OnGameUnpaused += ActiveHideStatBars;
        GameManager.Instance.Player.Stats.OnStaminaValueChanged += UpdateStaminaBar;
        GameManager.Instance.Player.Stats.OnHealthValueChanged += UpdateHealthBar;
        GameManager.Instance.Player.Stats.OnLeveledUp += LevelUpHUD;
        GameManager.Instance.Player.Health.OnStaminaValueChanged += UpdateStaminaBar;
        GameManager.Instance.Player.Health.OnHealthValueChanged += UpdateHealthBar;
        GameManager.Instance.Player.Health.OnPlayerDeath += EndGame;
        MissionManager.OnListEmptied += EndGame;

        // Update stats bars in the start of the gameplay scene
        UpdateHealthBar();
        UpdateStaminaBar();
    }

    private void PauseGame()
    {
        GameManager.Instance.Player.InputManager.SwitchInputMap(InputMap.Pause);
        Instantiate(pauseMenu, canvas.transform);
        ActiveHideStatBars();
    }

    private void ActiveHideStatBars()
    {
        barsGroup.SetActive(!barsGroup.activeSelf);
        missionObj.SetActive(!missionObj.activeSelf);
    }

    private void EndGame()
    {
        b_IsDone = true;
        GameManager.Instance.Player.Animator.SetInteger("Movement", -1);
        GameManager.Instance.Player.InputManager.SwitchInputMap(InputMap.Menu);
        Instantiate(endGameScreen, canvas.transform);
        GameplayEnd();
    }

    private void GameplayEnd()
    {
        // Unbinding functions to respective events
        GameManager.Instance.Player.InputManager.OnPause -= PauseGame;
        PauseMenu.OnGameUnpaused -= ActiveHideStatBars;
        GameManager.Instance.Player.Stats.OnStaminaValueChanged -= UpdateStaminaBar;
        GameManager.Instance.Player.Stats.OnHealthValueChanged -= UpdateHealthBar;
        GameManager.Instance.Player.Stats.OnLeveledUp -= LevelUpHUD;
        GameManager.Instance.Player.Health.OnStaminaValueChanged -= UpdateStaminaBar;
        GameManager.Instance.Player.Health.OnHealthValueChanged -= UpdateHealthBar;
        GameManager.Instance.Player.Health.OnPlayerDeath -= EndGame;

        //foreach (var enemy in enemies)
        //{
        //    enemy.GetComponent<EnemyHealth>().OnEnemyDeath -= DefeatEnemy;
        //    enemy.GetComponent<EnemyHealth>().OnEnemyDeath -= UpdateDefeatedEnemies;
        //}

        // Destroy gameobject after removing it of the ui manager action stack
        Destroy(gameObject);
    }
    
    // Update health bar in the HUD
    private void UpdateHealthBar()
    {
        healthBar.fillAmount = ((float)GameManager.Instance.Player.Stats.CurrentHealth) / ((float)GameManager.Instance.Player.Stats.MaxHealth);
    }

    // Update mana bar in the HUD
    private void UpdateManaBar()
    {
        manaBar.fillAmount = ((float) GameManager.Instance.Player.Stats.CurrentMana) / ((float) GameManager.Instance.Player.Stats.MaxMana);
    }

    private void LevelUpHUD(string text)
    {
        levelUpText.text = text;
        levelUpTextOverlay.text = text;
        levelUpObj.SetActive(true);
        StartCoroutine("LevelUpHUDHide");
    }

    private IEnumerator LevelUpHUDHide()
    {
        yield return new WaitForSecondsRealtime(3f);
        levelUpObj.SetActive(false);
    }

    // Update stamina bar in the HUD
    private void UpdateStaminaBar()
    {
        staminaBar.fillAmount = ((float) GameManager.Instance.Player.Stats.CurrentStamina) / ((float) GameManager.Instance.Player.Stats.MaxStamina);
    }
}