using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData EnemyDataSO;
    public EnemyHealth Health;
    private BehaviorTree myTree;
    private GroundCheck groundCheck;
    public float SearchRange;
    public TextMeshProUGUI EnemyName;

    void Awake()
    {
        EnemyDataSO = Instantiate(EnemyDataSO);
        groundCheck = GetComponent<GroundCheck>();
        myTree = new BehaviorTree();
        SetupTree();
    }

    private void OnEnable()
    {
        if (myTree.blackboard.data != null)
            transform.position = (Vector3) myTree.blackboard.data[$"{gameObject.name}_RespawnPos"];
        
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

    private void SetupTree()
    {
        SetupBlackboard();

        switch (EnemyDataSO.AggressivenessType)
        {
            case AggressivenessType.Passive: SetupPassiveEnemy(); break;            
            case AggressivenessType.Neutral: SetupNeutralEnemy(); break;            
            case AggressivenessType.Agressive: SetupAgressiveEnemy(); break;
        }
    }

    private void SetupBlackboard()
    {
        myTree.blackboard = new Blackboard();
        myTree.blackboard.data.Add($"EnemyName", gameObject.name);
        myTree.blackboard.data.Add($"{gameObject.name}_Data", EnemyDataSO);
        myTree.blackboard.data.Add($"{gameObject.name}_Transform", transform);
        myTree.blackboard.data.Add($"{gameObject.name}_StartPos", new Vector2(transform.localPosition.x, transform.localPosition.z));
        myTree.blackboard.data.Add($"{gameObject.name}_RespawnPos", transform.position);
        myTree.blackboard.data.Add($"{gameObject.name}_CurrentAwaitTime", 0f);
        //myTree.blackboard.data.Add($"{gameObject.name}_Animator", GetComponent<Animator>());
    }

    private void SetupPassiveEnemy()
    {
        myTree.root = new Selector(new List<Node> {
            new PlayerSearch(new Sequence(new List<Node> {
                new Flee(myTree.blackboard, SearchRange)
                // TODO - RETURN TO SPAWN SPOT BEHAVIOR
                }, myTree.blackboard),

            myTree.blackboard, SearchRange),

            new Sequence(new List<Node> {
                new Await(myTree.blackboard, 0.1f),
                new Patrol(myTree.blackboard)
            }, myTree.blackboard)

        }, myTree.blackboard);
    }

    private void SetupNeutralEnemy()
    {
        myTree.root = new Selector(new List<Node> {
            new Hurtness(new Sequence(new List<Node> {
                new MoveTowardsPlayer(myTree.blackboard, SearchRange),
                new Attack(myTree.blackboard, groundCheck, GetComponent<Rigidbody>()),
                new Await(myTree.blackboard, 2f)
            }, myTree.blackboard),

        myTree.blackboard, SearchRange, Health),

            new Sequence(new List<Node> {
                new Await(myTree.blackboard, Random.Range(0.5f, 5f)),
                new Patrol(myTree.blackboard)
            }, myTree.blackboard)

        }, myTree.blackboard);
    }

    private void SetupAgressiveEnemy()
    {
        myTree.root = new Selector(new List<Node> {
            new PlayerSearch(
                new Sequence(new List<Node> {
                    new MoveTowardsPlayer(myTree.blackboard, SearchRange),
                    new Attack(myTree.blackboard, groundCheck, GetComponent<Rigidbody>()),
                    new Await(myTree.blackboard, 12f)
                }, myTree.blackboard),

            myTree.blackboard, SearchRange),

            new Sequence(new List<Node> {
                new Await(myTree.blackboard, Random.Range(0.5f, 5f)),
                new Patrol(myTree.blackboard)
            }, myTree.blackboard)

        }, myTree.blackboard);
    }
}