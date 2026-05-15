using System;
using UnityEngine;

public enum EnemyType
{
    Mob,
    Boss
}

public enum EnemySpecies
{
    Slime,
    Spider,
    Goblin,
    Kobold
}

public enum AggressivenessType
{
    Neutral,
    Passive,
    Agressive
}

[CreateAssetMenu(menuName = "Scriptable Objects/Enemy Data", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    #region Atributes

    [HideInInspector] public int Level = 1;

    [Header("Enemy type")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemySpecies enemySpecies;
    [SerializeField] private AggressivenessType aggressivenessType;

    [Header("Enemy Stats")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int strength;
    [SerializeField] private int agility;
    [SerializeField] private int defense;
    [SerializeField] private int experienceGiven;

    #endregion

    #region Properties
    
    public EnemyType EnemyType { get { return enemyType; } }
    public EnemySpecies EnemySpecies { get { return enemySpecies; } }
    public AggressivenessType AggressivenessType { get {return aggressivenessType; } }
    public int MaxHealth { get { return maxHealth; } }
    public int Strength { get { return strength; } }
    public int Agility { get { return agility; } }
    public int Defense { get { return defense; } }
    public int ExperienceGiven { get { return experienceGiven; } }

    #endregion
}