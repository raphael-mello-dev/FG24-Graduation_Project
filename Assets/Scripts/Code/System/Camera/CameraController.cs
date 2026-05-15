using UnityEngine;

public class CameraController : ActionStack
{
    [Range(1f, 20f)]
    public float LookDistance = 5f;

    [Range(1f, 20f)]
    public float FollowSpeed = 6f;

    [Range(0f, 1f)]
    public float FollowAngle = 0.2f;

    public void SetFollowAngle(float value) => FollowAngle = value;
    public void SetLookDistance(float value) => LookDistance = value;

    public abstract class CameraAction : Action
    {
        protected CameraController controller;

        public CameraAction (CameraController cameraController)
        {
            controller = cameraController;
        }
    }

    public abstract class CameraActionBehavior : ActionBehaviour
    {
        protected CameraController controller;
        public Transform Transform => gameObject.transform;

        public const float MAX_ANGLE = 70f;

        public CameraActionBehavior (CameraController cameraController)
        {
            controller = cameraController;
        }
    }
}