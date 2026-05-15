using UnityEngine;

public class Attack : Node
{
    private Transform transform;
    private GroundCheck groundCheck;
    private Rigidbody rigidbody;
    private float jumpForce = 1f;

    public Attack(Blackboard aBlackBoard, GroundCheck aGroundCheck, Rigidbody aRigidbody) : base(null, aBlackBoard)
    {
        string enemyName = myBlackboard.data["EnemyName"] as string;
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
        return ReturnState.Running;
    }
}