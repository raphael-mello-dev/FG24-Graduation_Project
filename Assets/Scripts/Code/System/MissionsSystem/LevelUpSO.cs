using UnityEngine;

[CreateAssetMenu(fileName = "Level Up Mission", menuName = "Missions/Level Up")]
public class LevelUpSO : MissionSO
{
    public int NumberOfLevelUps;
    private int PlayerLevel;

    public override void OnStart()
    {
        base.OnStart();


        GameManager.Instance.Player.Stats.OnMissionUpdated += OnMissionUpdated;
        PlayerLevel = GameManager.Instance.Player.Stats.Level;
        DisplayMissionProgress($"Levels Upgraded: 0/{NumberOfLevelUps}");
    }

    public override void OnMissionUpdated()
    {
        DisplayMissionProgress($"Levels Upgraded: {GameManager.Instance.Player.Stats.Level - PlayerLevel}/{NumberOfLevelUps}");

        if (GameManager.Instance.Player.Stats.Level - PlayerLevel >= NumberOfLevelUps) isCompleted = true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        GameManager.Instance.Player.Stats.OnMissionUpdated += OnMissionUpdated;
    }
}