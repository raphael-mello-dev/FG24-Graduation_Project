using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Collider weaponCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            other.GetComponent<EnemyHealth>().TakeDamage(ApplyDamage());
    }

    private int ApplyDamage()
    {
        int damage = Mathf.RoundToInt(Random.Range(1, 5) * GameManager.Instance.Player.Stats.Strength);
        return damage;
    }

    public void AttackStart()
    {
        weaponCollider.enabled = true;
        GameManager.Instance.Player.Animator.SetFloat("Speed", 1f);
        GameManager.Instance.Player.Animator.SetTrigger("Attack");
    }
    
    public void AttackEnd() => StartCoroutine("ResetAttackTrigger");

    private IEnumerator ResetAttackTrigger()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        GameManager.Instance.Player.Animator.ResetTrigger("Attack");
        weaponCollider.enabled = false;
    }
}