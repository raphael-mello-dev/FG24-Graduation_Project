using UnityEngine;

public class Hurtness : Node
{
    private Node myChild;

    private string enemyName;
    private float searchRange;
    private Transform transform;
    private EnemyData enemyData;
    private EnemyHealth health;

    public Hurtness(Node aChild, Blackboard aBlackBoard, float range, EnemyHealth healthComponent) : base(null, aBlackBoard)
    {
        myChild = aChild;
        myChild.Parent = this;
        searchRange = range;

        enemyName = myBlackboard.data["EnemyName"] as string;
        transform = myBlackboard.data[$"{enemyName}_Transform"] as Transform;
        enemyData = myBlackboard.data[$"{enemyName}_Data"] as EnemyData;
        health = healthComponent;
    }

    public override ReturnState Evaluate()
    {
        float playerDistance = Vector3.Distance(transform.position, GameManager.Instance.Player.Transform.position);
        
        if (playerDistance < searchRange && health.CurrentHealth < enemyData.MaxHealth) return myChild.Evaluate();

        return ReturnState.Failure;
    }
}