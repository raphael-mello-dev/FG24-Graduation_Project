using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesScreen : MonoBehaviour
{
    #region Upgrade Buttons

    [Header("Health")]
    [SerializeField] private Button healthPlus;
    [SerializeField] private Button healthMinus;
    
    [Header("Stamina")]
    [SerializeField] private Button staminaPlus;
    [SerializeField] private Button staminaMinus;
    
    [Header("Mana")]
    [SerializeField] private Button manaPlus;
    [SerializeField] private Button manaMinus;
    
    [Header("Strength")]
    [SerializeField] private Button strengthPlus;
    [SerializeField] private Button strengthMinus;
    
    [Header("Agility")]
    [SerializeField] private Button agilityPlus;
    [SerializeField] private Button agilityMinus;
    
    [Header("Defense")]
    [SerializeField] private Button defensePlus;
    [SerializeField] private Button defenseMinus;

    #endregion

    [Header("Upgrades UI elements")]
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI upgradesStatus;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button resetButton;

    private PlayerStats playerStats;

    #region Temporary stats

    // Temporary stats for upgrades
    private int pointsSpent;
    private int extraPoints;

    [Header("Temporary Stats")]
    private int health;
    private int stamina;
    private int mana;
    private int strength;
    private int agility;
    private int defense;

    #endregion

    private void OnEnable()
    {
        playerStats = GameManager.Instance.Player.Stats;
        ResetStats();
        UpdateStatsText();
        upgradesStatus.text = "";
    }

    private void Start()
    {
        // Binding buttons on click events to their corresponding functions
        healthPlus.onClick.AddListener(IncreaseHealth);
        healthMinus.onClick.AddListener(DecreaseHealth);
        staminaPlus.onClick.AddListener(IncreaseStamina);
        staminaMinus.onClick.AddListener(DecreaseStamina);
        manaPlus.onClick.AddListener(IncreaseIntelligence);
        manaMinus.onClick.AddListener(DecreaseIntelligence);
        strengthPlus.onClick.AddListener(IncreaseStrength);
        strengthMinus.onClick.AddListener(DecreaseStrength);
        agilityPlus.onClick.AddListener(IncreaseAgility);
        agilityMinus.onClick.AddListener(DecreaseAgility);
        defensePlus.onClick.AddListener(IncreaseDefense);
        defenseMinus.onClick.AddListener(DecreaseDefense);
        applyButton.onClick.AddListener(ClickApply);
        resetButton.onClick.AddListener(ResetStats);
    }

    private void IncreaseStat(ref int stat)
    {
        stat++;
        pointsSpent++;
        UpdateStatsText();
    }
    
    private void DecreaseStat(ref int stat, int playerStat)
    {
        if (stat == playerStat) return;

        stat--;
        pointsSpent--;
        UpdateStatsText();
    }

    private void IncreaseHealth() => IncreaseStat(ref health);
    private void IncreaseStamina() => IncreaseStat(ref stamina);
    private void IncreaseIntelligence() => IncreaseStat(ref mana);
    private void IncreaseStrength() => IncreaseStat(ref strength);
    private void IncreaseAgility() => IncreaseStat(ref agility);
    private void IncreaseDefense() => IncreaseStat(ref defense);
    
    private void DecreaseHealth() => DecreaseStat(ref health, playerStats.Health);
    private void DecreaseStamina() => DecreaseStat(ref stamina, playerStats.Stamina);
    private void DecreaseIntelligence() => DecreaseStat(ref mana, playerStats.Mana);
    private void DecreaseStrength() => DecreaseStat(ref strength, playerStats.Strength);
    private void DecreaseAgility() => DecreaseStat(ref agility, playerStats.Agility);
    private void DecreaseDefense() => DecreaseStat(ref defense, playerStats.Defense);

    // Apply changes in player stats
    private void ClickApply()
    {
        if (pointsSpent <= extraPoints)
        {
            playerStats.AddOrSubtractStat(Stats.Health, (health - playerStats.Health));
            playerStats.AddOrSubtractStat(Stats.Stamina, (stamina - playerStats.Stamina));
            playerStats.AddOrSubtractStat(Stats.Intelligence, (mana - playerStats.Mana));
            playerStats.AddOrSubtractStat(Stats.Strength, (strength - playerStats.Strength));
            playerStats.AddOrSubtractStat(Stats.Agility, (agility - playerStats.Agility));
            playerStats.AddOrSubtractStat(Stats.Defense, (defense - playerStats.Defense));

            extraPoints = playerStats.ExtraPoints;
            UpdateStatsText();
            upgradesStatus.color = Color.green;
            upgradesStatus.text = "Stats updated!";
        }
        else
        {
            upgradesStatus.color = Color.red;
            upgradesStatus.text = "You don't have enough extra points!";
        }
    }

    // Reset modified stats to previous state
    private void ResetStats()
    {
        extraPoints = playerStats.ExtraPoints;
        pointsSpent = 0;
        health = playerStats.Health;
        stamina = playerStats.Stamina;
        mana = playerStats.Mana;
        strength = playerStats.Strength;
        agility = playerStats.Agility;
        defense = playerStats.Defense;

        upgradesStatus.text = "";
        
        UpdateStatsText();
    }

    // Update stats text
    private void UpdateStatsText()
    {
        statsText.text = $"\t\r\nHealth: {health}\t\t\t\t\t\tStamina:  {stamina}\r\n\r\nStrength: {strength}\t\t\t\t\tAgility: {agility}" +
            $"\r\n\r\nIntelligence: -\t\t\t\t\tConstitution: {defense}\r\n\r\n\r\n\r\nExtra Points: {extraPoints}";
    }
}