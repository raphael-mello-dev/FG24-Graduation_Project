using System.Collections.Generic;
using UnityEngine;

public class DefRegen : Node
{
    private EnemyHealth myHealth;
    private float healCooldown;

    public DefRegen(Blackboard aBlackBoard, EnemyHealth health) : base(null, aBlackBoard)
    {
        myHealth = health;
    }

    public override ReturnState Evaluate()
    {
        if (healCooldown > 0)
        {
            healCooldown -= Time.deltaTime;
            return ReturnState.Failure;
        }

        myHealth.RegenHealth(5);
        healCooldown = 5f;
        return ReturnState.Failure;
    }
}