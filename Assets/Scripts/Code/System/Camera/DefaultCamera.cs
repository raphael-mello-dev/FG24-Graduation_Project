using UnityEngine;

public class DefaultCamera : CameraController.CameraActionBehavior
{
    private PlayerController target { get {  return GameManager.Instance.Player; } }

    private Vector3 lookTarget
    {
        get { return target != null ? target.Transform.position + Vector3.up * 1.2f : Vector3.zero; }
    }

    private Vector3 eyeTarget
    {
        get
        {
            Vector3 Dir = -target.Transform.forward;
            Quaternion Rotation = Quaternion.AngleAxis(GameManager.Instance.CameraManager.FollowAngle * MAX_ANGLE, target.Transform.right);
            Dir = Rotation * Dir;
            return target != null ? lookTarget + Dir * GameManager.Instance.CameraManager.LookDistance : Vector3.forward;
        }
    }

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float sphereRadius;

    public DefaultCamera(CameraController cameraController) : base(cameraController)
    { controller = cameraController; }

    public override bool IsDone() { return false; }

    private void Start() => GameManager.Instance.CameraManager.PushAction(this);

    public override void OnUpdate()
    {
        base.OnUpdate();

        Transform.position += (eyeTarget - transform.position) * Time.deltaTime * GameManager.Instance.CameraManager.FollowSpeed;
        Transform.rotation = Quaternion.LookRotation(lookTarget - Transform.position);

        CameraCollisionSphereCast();
    }

    protected void CameraCollisionSphereCast()
    {
        Ray ray = new Ray(lookTarget, Vector3.Normalize(transform.position - lookTarget));
        RaycastHit hit;

        if (Physics.SphereCast(ray, sphereRadius, out hit, GameManager.Instance.CameraManager.LookDistance, groundLayer))
            transform.position = ray.GetPoint(hit.distance);
    }
}