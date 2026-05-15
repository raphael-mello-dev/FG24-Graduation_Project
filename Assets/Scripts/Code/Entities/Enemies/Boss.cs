using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public EnemyData EnemyDataSO;
    public EnemyHealth Health;
    private BehaviorTree myTree;
    private GroundCheck groundCheck;
    public float SearchRange;

    void Awake()
    {
        transform.localScale += new Vector3(3f, 3f, 3f);
        groundCheck = GetComponent<GroundCheck>();
        EnemyDataSO.Level = LevelDifficulty.GetBossLevel();
        myTree = new BehaviorTree();
        SetupTree();
    }

    private void OnEnable()
    {
        if (myTree.blackboard.data != null)
            transform.position = (Vector3)myTree.blackboard.data[$"{gameObject.name}_RespawnPos"];

        GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine("SuspendGravity");
    }

    void Update()
    {
        if (GameManager.Instance.UIManager.CurrentAction is not GameplayHUD || !groundCheck.IsGrounded) return;

        myTree.UpdateTree();
    }

    //Ducktape fix for gravity issue
    private IEnumerator SuspendGravity()
    {
        while (!groundCheck.IsGrounded) yield return null;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void SetupBlackboard()
    {
        myTree.blackboard = new Blackboard();
        myTree.blackboard.data.Add($"EnemyName", gameObject.name);
        myTree.blackboard.data.Add($"{gameObject.name}_Data", EnemyDataSO);
        myTree.blackboard.data.Add($"{gameObject.name}_Transform", transform);
        myTree.blackboard.data.Add($"{gameObject.name}_StartPos", new Vector2(transform.localPosition.x, transform.localPosition.z));
        myTree.blackboard.data.Add($"{gameObject.name}_RespawnPos", transform.position);
        myTree.blackboard.data.Add($"{gameObject.name}_SearchRange", SearchRange);
        myTree.blackboard.data.Add($"{gameObject.name}_CurrentAwaitTime", 0f);
    }

    private void SetupTree()
    {
        SetupBlackboard();

        myTree.root = new Selector(new List<Node> {
            new PlayerSearch(
                new StageSelector(new List<Node> {
                    // 1º Stage - Attack
                    new Sequence(new List<Node> {
                        new Await(myTree.blackboard, Random.Range(6f, 12f)),
                        new BossAttack(myTree.blackboard, AttackType.Sprint, 9f)
                    }, myTree.blackboard),
                    // 2º Stage - Defense / Regeneration
                    new Sequence(new List<Node> {
                        new DefRegen(myTree.blackboard, Health)
                    }, myTree.blackboard),
                    // 3º Stage - Attack
                    new Sequence(new List<Node> {
                        new Await(myTree.blackboard, Random.Range(3f, 6f)),
                        new BossAttack(myTree.blackboard, AttackType.Sprint, 12f)
                    }, myTree.blackboard)
                }, Health, myTree.blackboard),
            myTree.blackboard, SearchRange),

            new Sequence(new List<Node> {
                new Await(myTree.blackboard, 5f),
                new Patrol(myTree.blackboard)
            }, myTree.blackboard)

        }, myTree.blackboard);
    }
}