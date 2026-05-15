using System.Collections.Generic;
using UnityEngine;

public class StageSelector : Node
{
    private EnemyHealth myHealth;

    public StageSelector(List<Node> someChildren, EnemyHealth health, Blackboard aBlackBoard) : base(someChildren, aBlackBoard)
    {
        myChildren = someChildren;
        myHealth = health;
    }

    public override ReturnState Evaluate()
    {
        if (myHealth.GetHealthPercentage() >= 0.7)
        {
            myHealth.HasDamageReduction = false;
            return myChildren[0].Evaluate();
        }
        else if (myHealth.GetHealthPercentage() >= 0.35)
        {
            myHealth.HasDamageReduction = true;
            return myChildren[1].Evaluate();
        }
        else
        {
            myHealth.HasDamageReduction = false;
            return myChildren[2].Evaluate();
        }
    }
}