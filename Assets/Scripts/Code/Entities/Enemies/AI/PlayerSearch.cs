using UnityEngine;

public class PlayerSearch : Node
{
    private string enemyName;
    private float searchRange;
    private Node myChild;
    private Transform transform;

    public PlayerSearch(Node aChild, Blackboard aBlackBoard, float range) : base(null, aBlackBoard)
    {
        myChild = aChild;
        myChild.Parent = this;
        searchRange = range;

        enemyName = myBlackboard.data["EnemyName"] as string;
        transform = myBlackboard.data[$"{enemyName}_Transform"] as Transform;
    }

    public override ReturnState Evaluate()
    {
        float playerDistance = Vector3.Distance(transform.position, GameManager.Instance.Player.Transform.position);
        
        if (playerDistance > searchRange) return ReturnState.Failure;

        return myChild.Evaluate();
    }
}