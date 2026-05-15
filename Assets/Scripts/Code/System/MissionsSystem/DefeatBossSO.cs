using UnityEngine;

[CreateAssetMenu(fileName = "Defeat Boss Mission", menuName = "Missions/Defeat Boss")]
public class DefeatBossSO : MissionSO
{
    private GameObject boss;

    public override void OnStart()
    {
        base.OnStart();

        boss = GameObject.FindGameObjectWithTag("Boss");
        EnemyHealth.OnEnemyDeath += OnMissionUpdated;
        DisplayMissionProgress("Defeat Boss!");
    }

    public override void OnMissionUpdated()
    {
        base.OnMissionUpdated();

        isCompleted = true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        EnemyHealth.OnEnemyDeath -= OnMissionUpdated;
    }
}