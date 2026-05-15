using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Ground Check")]
    [SerializeField] private float groundCheckDistance = 0.87f;
    [SerializeField] private float groundSphereRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxSlopeAngle = 60f;

    [SerializeField] private bool isGrounded;
    public bool IsGrounded { get {  return isGrounded; } }
    
    private RaycastHit groundHit;
    public Vector3 GroundNormal => groundHit.normal;

    private bool isEnemy = false;
    private bool isBoss = false;

    private void Start()
    {
        if (GetComponent<Enemy>() != null)
            isEnemy = true;

        if (GetComponent<Boss>() != null)
            isBoss = true;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = GroundChecker();

        if ((isEnemy || isBoss) && !isGrounded)
            rb.AddForce(Vector3.down * 2f, ForceMode.Acceleration);
    }

    public bool GroundChecker()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        if (Physics.SphereCast(origin, groundSphereRadius, Vector3.down, out groundHit, groundCheckDistance, groundLayer, QueryTriggerInteraction.Ignore))
        {
            // Slope check
            float slopeAngle = Vector3.Angle(groundHit.normal, Vector3.up);
            return slopeAngle <= maxSlopeAngle;
        }
        else
            return false;
    }
}