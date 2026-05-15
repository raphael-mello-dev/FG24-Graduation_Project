using System;

public enum Stats
    {
        Health,
        Stamina,
        Intelligence,
        Strength,
        Agility,
        Defense
    }

public class PlayerStats
{
    private PlayerController Controller;

    #region Atributes

    private int level;

    private int health;
    private int maxHealth;
    private int currentHealth;

    private int stamina;
    private int maxStamina;
    private int currentStamina;

    private int mana;
    private int maxMana;
    private int currentMana;

    private int strength;

    private int agility;

    private int defense;

    private int experienceGained;
    private int extraPoints;

    #endregion

    #region Properties

    public int Level { get { return level; } }
    public int LevelsUpgraded { get; private set; }

    public int Health { get { return health; } }
    public int MaxHealth { get { return maxHealth; } }
    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    
    public int Stamina { get { return stamina; } }
    public int MaxStamina { get { return maxStamina; } }
    public int CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = value; }
    }
    
    public int Mana { get { return mana; } }
    public int MaxMana { get { return maxMana; } }
    public int CurrentMana {  get { return currentMana; } }

    public int Strength { get { return strength; } }
    public int Agility { get { return agility; } }
    public int Defense { get { return defense; } }
    public int ExperienceGained {  get { return experienceGained; } }
    public int ExtraPoints { get { return extraPoints; } }

    #endregion

    public event Action OnStaminaValueChanged;
    public event Action OnHealthValueChanged;
    public event Action<string> OnLeveledUp;
    public event Action OnMissionUpdated;

    public PlayerStats(PlayerController controller) => Controller = controller;

    // Setup player stats by save or in new game
    public void LoadStats()
    {
        var player = SaveManager.Instance.GameData.Player;

        level = player.Level;
        LevelsUpgraded = 0;

        health = player.Health;
        maxHealth = player.MaxHealth;
        currentHealth = player.CurrentHealth;

        stamina = player.Stamina;
        maxStamina = player.MaxStamina;
        currentStamina = player.CurrentStamina;

        mana = player.Mana;
        currentMana = player.CurrentMana;

        strength = player.Strength;
        agility = player.Agility;
        defense = player.Defense;
        experienceGained = player.ExperienceGained;
        extraPoints = player.ExtraPoints;
    }

    // New game player stats
    public void NewGameSetupStats()
    {
        level = 0;
        LevelsUpgraded = 0;

        health = 0;
        maxHealth = 100;
        currentHealth = maxHealth;

        stamina = 0;
        maxStamina = 30;
        currentStamina = maxStamina;

        mana = 0;
        currentMana = maxMana;

        strength = 1;
        agility = 1;
        defense = 0;
        experienceGained = 0;
        extraPoints = 0;
    }

    // Add or subtract a specific stat
    public void AddOrSubtractStat(Stats stat, int value)
    {
        // Checking if there are extra points left to add stats
        if (value == 0 || extraPoints - value < 0) return;

        //Updating stat
        switch (stat)
        {
            case Stats.Health:
                health += value;
                maxHealth += value * 10;
                currentHealth += value * 10;
            break;

            case Stats.Stamina:
                stamina += value;
                maxStamina += value * 3;
                currentStamina += value * 3;
            break;
            
            case Stats.Intelligence:
                mana += value;
                maxMana += value * 5;
                currentMana += value * 5;
            break;
            
            case Stats.Strength:
                strength += value;
            break;
            
            case Stats.Agility:
                agility += value;
            break;
            
            case Stats.Defense:
                defense += value;
            break;
        }

        extraPoints -= value;
    }
    
    public void AddExperience(int value)
    {
        if (experienceGained + value >= (level + 1) * 10)
        {
            experienceGained += value;
            experienceGained -= ((level + 1) * 10);
            value = 0;
            level++;
            LevelsUpgraded++;
            AddExperience(value);
        }
        else
            experienceGained += value;
        
        LevelUp();
    }

    public void LevelUp()
    {
        if (LevelsUpgraded == 0)
            return;
        else if (LevelsUpgraded == 1)
            OnLeveledUp?.Invoke("Level Up!");
        else
            OnLeveledUp?.Invoke($"Level Up! (x{LevelsUpgraded})");

        OnMissionUpdated?.Invoke();
        extraPoints += LevelDifficulty.GetExtraPoints() * LevelsUpgraded;

        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
        OnHealthValueChanged?.Invoke();
        OnStaminaValueChanged?.Invoke();
        
        LevelsUpgraded = 0;
    }
}