using UnityEngine;

public class Patrol : Node
{
    private string enemyName;
    private Transform transform;
    private Vector2 spawnPosition, patrolPosition;
    private Vector3[] Directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.zero };
    private Vector3 patrolDirection;
    private float patrolDistance;

    public Patrol(Blackboard aBlackBoard) : base(null, aBlackBoard)
    {
        enemyName = myBlackboard.data["EnemyName"] as string;
        transform = myBlackboard.data[$"{enemyName}_Transform"] as Transform;
        spawnPosition = (Vector2)myBlackboard.data[$"{enemyName}_StartPos"];

        int directionsCount = Mathf.RoundToInt(Random.Range(1, 4));
        Vector3 Direction = Vector3.zero;

        for (int i = 0; i < directionsCount; i++)
        {
            Vector3 currentDirection = Directions[Mathf.RoundToInt(Random.Range(0, Directions.Length))];
            if (currentDirection != Vector3.zero && Direction + currentDirection != Vector3.zero)
                Direction += currentDirection;
        }

        patrolDistance = Random.Range(3, 15);
        patrolPosition = spawnPosition + new Vector2(Direction.x, Direction.z) * patrolDistance;
        patrolDirection = (new Vector3(patrolPosition.x, transform.localPosition.y, patrolPosition.y) - transform.localPosition).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(patrolDirection.x, 0, patrolDirection.z));
    }

    public override ReturnState Evaluate()
    {
        if (patrolPosition == spawnPosition) return ReturnState.Running;

        transform.position += patrolDirection * Time.deltaTime * 2f;
        Vector2 currentPos = new Vector2(transform.localPosition.x, transform.localPosition.z);

        if (Vector2.Distance(currentPos, spawnPosition) < 0.1f)
        {
            GetNewPatrolDirection((new Vector3(patrolPosition.x, transform.localPosition.y, patrolPosition.y) - transform.localPosition).normalized);
        }
        else if (Vector2.Distance(currentPos, patrolPosition) < 0.1f)
        {
            GetNewPatrolDirection((new Vector3(spawnPosition.x, transform.localPosition.y, spawnPosition.y) - transform.localPosition).normalized);
        }
        else if (Vector2.Distance(currentPos, spawnPosition) > (patrolDistance + 1))
        {
            GetNewPatrolDirection((new Vector3(spawnPosition.x, transform.localPosition.y, spawnPosition.y) - transform.localPosition).normalized);
        }

        return ReturnState.Running;
    }

    private ReturnState GetNewPatrolDirection(Vector3 aDirection)
    {
        patrolDirection = aDirection;
        transform.rotation = Quaternion.LookRotation(new Vector3(patrolDirection.x, 0, patrolDirection.z));
        myBlackboard.data[$"{enemyName}_CurrentAwaitTime"] = 0f;
        return ReturnState.Failure;
    }
}