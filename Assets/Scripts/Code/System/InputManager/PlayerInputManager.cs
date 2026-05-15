using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Input action maps enum
public enum InputMap
{
    Menu,
    Gameplay,
    Pause
}

public class PlayerInputManager
{
    private Controls controls;
    private PlayerController controller;

    public float movement => controls.Gameplay.Walk.ReadValue<float>();
    public Vector2 rotation => controls.Gameplay.Rotate.ReadValue<Vector2>();
    public float isRunning => controls.Gameplay.Run.ReadValue<float>();

    public event Action OnPause;
    public event Action OnUnpause;

    public PlayerInputManager(PlayerController player)
    {
        controls = new Controls();
        controls.Menu.Enable();

        controller = player;

        controls.Gameplay.Pause.performed += PausePerformed;
        controls.Gameplay.Jump.performed += JumpPerformed;
        controls.Gameplay.Attack.performed += AttackPerformed;
        controls.Gameplay.Save.performed += SavePerformed;
        
        controls.Pause.UnPause.performed += UnpausePerformed;
    }

    // function for switching between input action maps
    public void SwitchInputMap(InputMap map)
    {
        // Disabling all input maps
        controls.Menu.Disable();
        controls.Gameplay.Disable();
        controls.Pause.Disable();

        // Activating only chosen input map
        switch (map)
        {
            case InputMap.Menu:
                controls.Menu.Enable();
            break;
            
            case InputMap.Gameplay:
                controls.Gameplay.Enable();
            break;

            case InputMap.Pause:
                controls.Pause.Enable();
            break;

            default:
            break;
        }
    }

    private void PausePerformed(InputAction.CallbackContext context) => OnPause?.Invoke();

    private void UnpausePerformed(InputAction.CallbackContext context) => OnUnpause?.Invoke();

    private void JumpPerformed(InputAction.CallbackContext context) => controller.PushAction(new PlayerJump(controller));
    
    private void AttackPerformed(InputAction.CallbackContext context)
    {
        controller.Weapon.AttackStart();
        controller.Weapon.AttackEnd();
    }

    private void SavePerformed(InputAction.CallbackContext context)
    {
        if (SaveManager.Instance.CanBeSaved)
            SaveManager.Instance.SaveGame();
    }
}