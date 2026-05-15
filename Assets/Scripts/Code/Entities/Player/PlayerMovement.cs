using UnityEngine;

public class PlayerMovement : PlayerController.PlayerAction
{
    private float walkSpeed = 2f;
    private float runSpeed = 5f;
    private float rotationSpeed = 120f;
    private float animSpeed = 0;

    public PlayerMovement(PlayerController player) : base(player) => controller = player;

    public override bool IsDone() { return false; }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (GameManager.Instance.UIManager.CurrentAction is not GameplayHUD) return;

        Movement();
        Rotate();        
    }

    private void Movement()
    {
        //float speedTarget = 0;

        // Checking if player is running or walking and adjusting movement according to output
        if (controller.InputManager.isRunning > 0.5f && controller.Stats.CurrentStamina > 0 && controller.InputManager.movement > 0)
        {
            //speedTarget = 1f;
            controller.Animator.SetInteger("Movement", 2);
            controller.Transform.position += controller.Transform.forward * controller.InputManager.movement * (controller.Stats.Agility / 2f + runSpeed) * Time.deltaTime;
        }
        else if (controller.InputManager.movement > 0)
        {
            //speedTarget = 0.5f;
            controller.Animator.SetInteger("Movement", 1);
            controller.Transform.position += controller.Transform.forward * controller.InputManager.movement * (controller.Stats.Agility / 5 + walkSpeed) * Time.deltaTime;
        }
        //else
        //    speedTarget = 0f;
        else if (controller.InputManager.movement == 0)
            controller.Animator.SetInteger("Movement", 0);
        //animSpeed = Mathf.MoveTowards(animSpeed, speedTarget, Time.deltaTime * 2);
        //controller.Animator.SetFloat("Speed", animSpeed);
    }

    private void Rotate()
    {
        // Rotating player according to input
        controller.Transform.Rotate(Vector3.up, controller.InputManager.rotation.x * rotationSpeed * Time.deltaTime);
        //if (controller.InputManager.movement == 0 && controller.InputManager.rotation.x != 0)
        //    controller.Animator.SetInteger("Movement", 3);
    }
}