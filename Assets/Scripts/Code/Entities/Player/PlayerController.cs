using UnityEngine;

public class PlayerController : ActionStack
{
    public abstract class PlayerAction : Action
    {
        protected PlayerController controller;

        public PlayerAction(PlayerController player)
        {
            controller = player;
        }
    }

    public PlayerInputManager InputManager { get; private set; }
    public PlayerStats Stats { get; private set; }

    #region Properties

    public string Name { get; private set; }

    public string PrefabPath { get; private set; }

    public GameObject GameObject { get; private set; }

    public Transform Transform { get; private set; }
    
    public Rigidbody Rigidbody { get; private set; }

    public Animator Animator { get; private set; }

    public PlayerHealth Health { get; private set; }

    public Weapon Weapon { get; private set; }

    #endregion

    public PlayerController()
    {
        InputManager = new PlayerInputManager(this);
        Stats = new PlayerStats(this);
        Health = new PlayerHealth(this);
    }

    public override void UpdateActions()
    {
        base.UpdateActions();
        Health.OnUpdate();
    }

    // Function for setting the player data
    public void SetPlayer(string playerName, string playerPath)
    {
        PrefabPath = playerPath;
        Name = playerName;
    }

    public void SetPlayerGameObject(GameObject player)
    {
        GameObject = player;
        Transform = GameObject.transform;
        Rigidbody = GameObject.GetComponent<Rigidbody>();
        Animator = GameObject.GetComponent<Animator>();
        Weapon = Transform.GetComponentInChildren<Weapon>();
    }
}