using UnityEngine;

public class Attack : Node
{
    private string enemyName;
    private Transform transform;
    private GroundCheck groundCheck;
    private Rigidbody rigidbody;
    private float jumpForce = 1f;

    public Attack(Blackboard aBlackBoard, GroundCheck aGroundCheck, Rigidbody aRigidbody) : base(null, aBlackBoard)
    {
        enemyName = myBlackboard.data["EnemyName"] as string;
        transform = myBlackboard.data[$"{enemyName}_Transform"] as Transform;
        groundCheck = aGroundCheck;
        rigidbody = aRigidbody;
    }

    public override ReturnState Evaluate()
    {
        if (!groundCheck.IsGrounded)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            return ReturnState.Running;
        }

        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rigidbody.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
        myBlackboard.data[$"{enemyName}_CurrentAwaitTime"] = 0f;
        return ReturnState.Running;
    }
}