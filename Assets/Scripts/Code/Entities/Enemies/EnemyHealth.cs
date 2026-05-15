using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Image myHealthBar;
    [SerializeField] private EnemyData EnemyDataSO;
    
    private int maxHealth;
    private int currentHealth;
    public int CurrentHealth {  get { return currentHealth; } }
    public bool HasDamageReduction { get; set; }

    public static event Action OnEnemyDeath;
    public event Action<GameObject> OnEnemyRespawned;

    private void OnEnable()
    {
        maxHealth = EnemyDataSO.MaxHealth * EnemyDataSO.Level;
        RestoreHealth();
        UpdateHealthBar();
    }

    public void RestoreHealth() => currentHealth = maxHealth;

    public void RegenHealth(int someHealth)
    {
        currentHealth += someHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float value)
    {
        int realValue = (HasDamageReduction) ? Mathf.RoundToInt(value * (1 - LevelDifficulty.GetDamageReduction())) : (int) value;

        if ((currentHealth - realValue) <= 0)
        {
            OnEnemyDeath?.Invoke();
            OnEnemyRespawned?.Invoke(gameObject);
            GameManager.Instance.Player.Stats.AddExperience(EnemyDataSO.ExperienceGiven * EnemyDataSO.Level);
            gameObject.SetActive(false);
        }

        currentHealth -= realValue;
        UpdateHealthBar();
    }

    public void UpdateHealthBar() => myHealthBar.fillAmount = GetHealthPercentage();

    public float GetHealthPercentage()
    {
        return ((float)currentHealth / (float)maxHealth);
    }
}