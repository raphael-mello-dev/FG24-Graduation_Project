using UnityEngine;

public class MoveTowardsPlayer : Node
{
    private string enemyName;
    private float searchRange;
    private Transform transform;
    private float speed;

    public MoveTowardsPlayer(Blackboard aBlackBoard, float range) : base(null, aBlackBoard)
    {
        searchRange = range;
        enemyName = myBlackboard.data["EnemyName"] as string;
        EnemyData data = myBlackboard.data[$"{enemyName}_Data"] as EnemyData;
        transform = myBlackboard.data[$"{enemyName}_Transform"] as Transform;
        speed = (float) data.Agility;
    }

    public override ReturnState Evaluate()
    {        
        Vector3 playerDirection = GameManager.Instance.Player.Transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
        float playerDistance = Vector3.Distance(transform.position, GameManager.Instance.Player.Transform.position);

        if (playerDistance <= 1f)
            return ReturnState.Success;

        transform.position += playerDirection * (speed / 2.5f) * Time.deltaTime;
        return ReturnState.Failure;
    }
}