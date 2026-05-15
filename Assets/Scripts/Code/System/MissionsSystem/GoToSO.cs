using UnityEngine;

[CreateAssetMenu(fileName = "Go To Mission", menuName = "Missions/Go to a Location")]
public class GoToSO : MissionSO
{
    public Vector3 TargetPosition;
    private float currentDistance; // Distance of the player to the target

    public override void OnStart()
    {
        base.OnStart();
        CalculateDistanceToPoint();
    }

    public override void OnMissionUpdated()
    {
        CalculateDistanceToPoint();
        if (currentDistance < 30f) isCompleted = true;
    }

    private void CalculateDistanceToPoint()
    {
        Vector3 currentPos = GameManager.Instance.Player.Transform.localPosition;
        currentDistance = Vector2.Distance(new Vector2(currentPos.x, currentPos.z), new Vector2(TargetPosition.x, TargetPosition.z));
        DisplayMissionProgress($"Go to location: {currentDistance:F0}m left"); // F2 - two decimal places after the comma
    }
}