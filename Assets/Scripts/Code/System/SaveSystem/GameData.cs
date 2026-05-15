using UnityEngine;

[System.Serializable]
public class GameData
{
    public SettingsData Settings;
    public PlayerData Player;

    public GameData()
    {
        Settings = new SettingsData();
        Player = new PlayerData();
    }

    public void SetData()
    {
        Settings.SetData();
        Player.SetData();
    }
}

[System.Serializable]
public class SettingsData
{
    public void SetData()
    {

    }
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public string PrefabPath;
    public float[] Position;
    public float[] Rotation;

    public int Level;

    public int Health;
    public int MaxHealth;
    public int CurrentHealth;

    public int Stamina;
    public int MaxStamina;
    public int CurrentStamina;

    public int Mana;
    public int MaxMana;
    public int CurrentMana;

    public int Strength;
    public int Agility;
    public int Defense;

    public int ExperienceGained;
    public int ExtraPoints;
    
    public void SetData()
    {
        PlayerController player = GameManager.Instance.Player;

        Name = player.Name;
        PrefabPath = player.PrefabPath;
        
        if (Position == null)
            Position = new float[3];
        
        if (Rotation == null)
            Rotation = new float[3];

        Position[0] = player.Transform.position.x;
        Position[1] = player.Transform.position.y;
        Position[2] = player.Transform.position.z;

        Rotation[0] = player.Transform.rotation.x;
        Rotation[1] = player.Transform.rotation.y;
        Rotation[2] = player.Transform.rotation.z;

        Level = player.Stats.Level;

        Health = player.Stats.Health;
        MaxHealth = player.Stats.MaxHealth;
        CurrentHealth = player.Stats.CurrentHealth;

        Stamina = player.Stats.Stamina;
        MaxStamina = player.Stats.MaxStamina;
        CurrentStamina = player.Stats.CurrentStamina;

        Mana = player.Stats.Mana;
        MaxMana = player.Stats.MaxMana;
        CurrentMana = player.Stats.CurrentMana;

        Strength = player.Stats.Strength;
        Agility = player.Stats.Agility;
        Defense = player.Stats.Defense;

        ExperienceGained = player.Stats.ExperienceGained;
        ExtraPoints = player.Stats.ExtraPoints;
    }
}