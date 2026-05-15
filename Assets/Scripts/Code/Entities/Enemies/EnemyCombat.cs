using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private EnemyData enemyData;

    private void Start() => enemyData = GetComponentInParent<Enemy>().EnemyDataSO;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.Player.Health.TakeDamage(ApplyDamage());
    }

    private int ApplyDamage()
    {
        int damage = Mathf.RoundToInt(Random.Range(1, 3) * enemyData.Strength * enemyData.Level);
        return damage;
    }
}