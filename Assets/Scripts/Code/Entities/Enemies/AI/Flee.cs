using UnityEngine;

public class Flee : Node
{
    private string enemyName;
    private float searchRange;
    private Transform transform;
    private float speed;

    public Flee(Blackboard aBlackBoard, float range) : base(null, aBlackBoard)
    {
        searchRange = range;
        enemyName = myBlackboard.data["EnemyName"] as string;
        transform = myBlackboard.data[$"{enemyName}_Transform"] as Transform;
        EnemyData data = myBlackboard.data[$"{enemyName}_Data"] as EnemyData;
        speed = (float) data.Agility;
    }

    public override ReturnState Evaluate()
    {
        Vector3 playerDirection = GameManager.Instance.Player.Transform.position - transform.position;
        float playerDistance = Vector3.Distance(transform.position, GameManager.Instance.Player.Transform.position);

        if (playerDistance > searchRange)
            return ReturnState.Failure;

        transform.rotation = Quaternion.LookRotation(-new Vector3(playerDirection.x, 0, playerDirection.z));
        transform.position += -playerDirection * (speed / 2.5f) * Time.deltaTime;

        return ReturnState.Success;
    }
}