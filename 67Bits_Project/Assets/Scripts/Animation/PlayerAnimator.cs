using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] AbstractMover _mover = null;
    [SerializeField] Animator _anim = null;

    private readonly int X_VELOCITY_HASH = Animator.StringToHash("XVelocity");

    private void LateUpdate()
    {
        _anim.SetFloat(X_VELOCITY_HASH, _mover.GetNormalizedVelocity());
    }
}
