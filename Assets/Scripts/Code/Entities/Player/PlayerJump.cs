using System;
using System.Collections;
using UnityEngine;

public class PlayerJump : PlayerController.PlayerAction
{
    private bool b_IsDone = false;
    private float jumpPower = 10f;
    private float jumpDuration;
    private float walkSpeed = 2f;
    private float rotationSpeed = 120f;

    public PlayerJump(PlayerController player) : base(player)
    {
        controller = player;
        jumpDuration = 2.25f;
    }

    public override bool IsDone() { return b_IsDone; }

    public override void OnBegin(bool bIsFirstTime)
    {
        base.OnBegin(bIsFirstTime);
        Jump();
    }

    public override void OnEnd() => base.OnEnd();

    public override void OnUpdate()
    {
        base.OnUpdate();

        // Enabling movement while jumping
        controller.Transform.position += controller.Transform.forward * controller.InputManager.movement * (controller.Stats.Agility / 5 + walkSpeed) * Time.deltaTime;
        controller.Transform.Rotate(Vector3.up, controller.InputManager.rotation.x * rotationSpeed * Time.deltaTime);

        if (jumpDuration > 0)
            jumpDuration -= Time.deltaTime;
        else
            JumpEnd();
    }

    private void Jump()
    {
        if (!controller.GameObject.GetComponent<GroundCheck>().IsGrounded || controller.Stats.CurrentStamina - 5 < 0)
        {
            b_IsDone = true;
            return;
        }

        controller.Animator.SetFloat("Speed", 0.5f);
        controller.Animator.SetTrigger("Jump");
        controller.Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        controller.Health.ManageStamina(-5);
    }

    // Jump end removes the jump action from the stack
    private void JumpEnd()
    {
        controller.Animator.ResetTrigger("Jump");
        b_IsDone = true;
    }
}