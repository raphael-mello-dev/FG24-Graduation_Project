using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesList = new List<GameObject>();
    [SerializeField] private List<GameObject> enemiesTotal = new List<GameObject>();

    [SerializeField] private bool CanEnemiesRespawn;
    [Range(5f, 60f), SerializeField] private float respawnTime;

    private void OnDestroy()
    {
        foreach (GameObject enemy in enemiesTotal)
            enemy.GetComponent<EnemyHealth>().OnEnemyRespawned -= CallRespawn;
    }

    void Start()
    {
        CanEnemiesRespawn = LevelDifficulty.GetCanEnemiesRespawn();

        float[] xPos = { (transform.position.x - LevelDifficulty.GetMaxSpawnRange()), (transform.position.x + LevelDifficulty.GetMaxSpawnRange()) };
        float[] zPos = { (transform.position.z - LevelDifficulty.GetMaxSpawnRange()), (transform.position.z + LevelDifficulty.GetMaxSpawnRange()) };

        for (int i = 0; i < LevelDifficulty.GetAmountOfEnemiesSpawned(); i++)
        {
            Vector2 spawnLocation = new Vector2(Random.Range(xPos[0], xPos[1]), Random.Range(zPos[0], zPos[1]));
            GameObject enemy = Instantiate(enemiesList[Random.Range(0, enemiesList.Count)].gameObject, new Vector3(spawnLocation.x, transform.position.y, spawnLocation.y), Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().EnemyDataSO.Level = Random.Range(1, LevelDifficulty.GetEnemiesMaxLevel() + 1);
            enemy.GetComponent<Enemy>().EnemyName.text += $" Lv. {enemy.GetComponent<Enemy>().EnemyDataSO.Level}";
            enemy.GetComponent<EnemyHealth>().OnEnemyRespawned += CallRespawn;
            enemiesTotal.Add(enemy);
        }
    }
    private IEnumerator RespawnEnemy(GameObject enemy)
    {
        if (!CanEnemiesRespawn) yield return null;
        yield return new WaitForSecondsRealtime(respawnTime);
        enemy.SetActive(true);
    }

    private void CallRespawn(GameObject enemy) => StartCoroutine(RespawnEnemy(enemy));
}