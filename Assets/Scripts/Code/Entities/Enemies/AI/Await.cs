using UnityEngine;

public class Await : Node
{
    private float waitTime;
    private float currentTime;

    public Await(Blackboard aBlackBoard, float waitTime) : base(null, aBlackBoard)
    {
        this.waitTime = waitTime;
    }

    public override ReturnState Evaluate()
    {
        currentTime = (float) myBlackboard.data[$"{myBlackboard.data["EnemyName"]}_CurrentAwaitTime"];

        if (currentTime < waitTime)
        {
            currentTime += Time.deltaTime;
            myBlackboard.data[$"{myBlackboard.data["EnemyName"]}_CurrentAwaitTime"] = currentTime;
            return ReturnState.Failure;
        }

        return ReturnState.Success;
    }
}