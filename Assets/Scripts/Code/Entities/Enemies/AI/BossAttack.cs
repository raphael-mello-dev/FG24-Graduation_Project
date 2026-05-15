using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Sprint,
    Jump
}

public class BossAttack : Node
{
    private AttackType attackType;

    private string bossName;
    private Transform transform;
    private float searchRange;
    Vector3 playerPosition, direction;
    Vector2 spawnPosition;

    float force;

    private int jumpAttempts, jumpMaxAttempts = 5;

    public BossAttack(Blackboard aBlackBoard, AttackType attackType, float aForce = 0, GroundCheck aGroundCheck = null, Rigidbody aRigidbody = null) : base(null, aBlackBoard)
    {
        this.attackType = attackType;
        force = aForce;

        bossName = myBlackboard.data["EnemyName"] as string;
        transform = myBlackboard.data[$"{bossName}_Transform"] as Transform;
        searchRange = (float) myBlackboard.data[$"{bossName}_SearchRange"];
        spawnPosition = (Vector2) myBlackboard.data[$"{bossName}_StartPos"];
    }

    public override ReturnState Evaluate()
    {
        if (attackType == AttackType.Sprint)
            return SprintAttack();
        else if (attackType == AttackType.Jump)
            return JumpAttack();
        else
            return ReturnState.Running;
    }

    private ReturnState SprintAttack()
    {
        transform.position += direction * Time.deltaTime * force;
        Vector2 currentPos = new Vector2(transform.localPosition.x, transform.localPosition.z);

        if (Vector2.Distance(currentPos, spawnPosition) < 0.2f || playerPosition == Vector3.zero)
        {
            Vector3 playerPosVector = GameManager.Instance.Player.Transform.localPosition;
            playerPosition = new Vector3(playerPosVector.x, transform.localPosition.y, playerPosVector.z);
            direction = (playerPosition - transform.localPosition).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            myBlackboard.data[$"{bossName}_CurrentAwaitTime"] = 0f;
            return ReturnState.Failure;
        }
        else if (Vector2.Distance(currentPos, new Vector2(playerPosition.x, playerPosition.z)) < 0.2f ||
                Vector2.Distance(currentPos, spawnPosition) > searchRange)
        {
            direction = (new Vector3(spawnPosition.x, transform.localPosition.y, spawnPosition.y) - transform.localPosition).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            myBlackboard.data[$"{bossName}_CurrentAwaitTime"] = (float) myBlackboard.data[$"{bossName}_CurrentAwaitTime"] / 3;
            return ReturnState.Failure;
        }

        return ReturnState.Running;
    }

    private ReturnState JumpAttack()
    {
        return ReturnState.Success;
    }
}