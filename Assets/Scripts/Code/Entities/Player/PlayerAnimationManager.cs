using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private CapsuleCollider playerCollider;

    void Update()
    {
        //if (GameManager.Instance.Player.Animator.GetInteger("Movement") != 0)
        //    playerCollider.center = new Vector3(0, 0.91f, 0);
        //else
        //    playerCollider.center = new Vector3(0, 0.73f, 0);
    }
}