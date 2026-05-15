using UnityEngine;

public enum EnemiesToKill
{
    One,
    Amount,
    All
}

[CreateAssetMenu(fileName = "Kill Enemies Mission", menuName = "Missions/Kill Enemies")]
public class KillEnemiesSO : MissionSO
{
    [SerializeField] private EnemiesToKill enemiesToKill;
    public int RequiredKills;
    private int currentKills = 0;
    private GameObject[] enemies;

    private string missionText;

    public override void OnStart()
    {
        base.OnStart();


        switch (enemiesToKill)
        {
            case EnemiesToKill.One:
                missionText = $"Kill Enemy";
                enemies = GameObject.FindGameObjectsWithTag("FirstEnemy");
                break;

            case EnemiesToKill.Amount:
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                missionText = $"Killed Enemies: 0/{RequiredKills}";
                break;

            case EnemiesToKill.All:
                enemies = GameObject.FindGameObjectsWithTag("Enemy");
                RequiredKills = enemies.Length;
                missionText = $"Killed Enemies: 0/{RequiredKills}";
                break;
        }

        EnemyHealth.OnEnemyDeath += OnMissionUpdated;
        DisplayMissionProgress(missionText);
    }

    public override void OnMissionUpdated()
    {
        currentKills++;

        if (enemiesToKill != EnemiesToKill.One)
            DisplayMissionProgress($"Killed Enemies: {currentKills}/{RequiredKills}");

        if (currentKills == RequiredKills) isCompleted = true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        
        EnemyHealth.OnEnemyDeath -= OnMissionUpdated;
    }
}