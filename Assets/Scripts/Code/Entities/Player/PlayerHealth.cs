using System;
using UnityEngine;

public class PlayerHealth
{
    private PlayerController controller;

    // Regen time
    private float stamWaitTime = 1f;
    private float healWaitTime = 3f;
    // Stamina cost (subtract) time
    private float subWaitTime = 0.5f;

    private bool canHeal;

    public event Action OnStaminaValueChanged;
    public event Action OnHealthValueChanged;
    public event Action OnPlayerDeath;

    public PlayerHealth(PlayerController player)
    {
        controller = player;
        canHeal = LevelDifficulty.GetCanHeal(); 
    }

    public void OnUpdate()
    {
        if (GameManager.Instance.UIManager.CurrentAction is not GameplayHUD) return;

        StaminaUsage();
        HealthRegen();
        StaminaRegen();
    }

    private void StaminaUsage()
    {
        // Checking if player is running or walking and adjusting movement according to output
        if (controller.InputManager.isRunning > 0.5f && controller.Stats.CurrentStamina > 0 && controller.InputManager.movement > 0)
        {
            if (subWaitTime <= 0)
            {
                ManageStamina(-1);
                subWaitTime = 0.5f;
            }
            else
                subWaitTime -= Time.deltaTime;
        }
    }

    // Manages current stamina of the player
    public void ManageStamina(int value)
    {
        controller.Stats.CurrentStamina = controller.Stats.CurrentStamina + value;
        OnStaminaValueChanged?.Invoke();
    }

    private void ManageHealth(int value)
    {
        controller.Stats.CurrentHealth = controller.Stats.CurrentHealth + value;
        OnHealthValueChanged?.Invoke();
    }

    public void TakeDamage(int value)
    {
        if ((controller.Stats.CurrentHealth - value) <= 0)
        {
            ManageHealth(-controller.Stats.CurrentHealth);
            OnPlayerDeath?.Invoke();
            return;
        }

        ManageHealth(-value);
    }

    private void HealthRegen()
    {
        if (!canHeal) return;

        if (controller.Stats.CurrentHealth < controller.Stats.MaxHealth && healWaitTime <= 0)
        {
            ManageHealth(1);
            healWaitTime = 3f;
        }
        else
            healWaitTime -= Time.deltaTime;
    }
    
    private void StaminaRegen()
    {
        if (controller.InputManager.isRunning < 0.5f && controller.Stats.CurrentStamina < controller.Stats.MaxStamina && stamWaitTime <= 0)
        {
            ManageStamina(1);
            stamWaitTime = 1f;
        }
        else
            stamWaitTime -= Time.deltaTime;
    }
}