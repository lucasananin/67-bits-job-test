using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] AbstractMover _mover = null;
    [SerializeField] Animator _anim = null;

    private readonly int X_VELOCITY_HASH = Animator.StringToHash("XVelocity");
    private readonly int PUNCH_HASH = Animator.StringToHash("Punch");

    private void LateUpdate()
    {
        _anim.SetFloat(X_VELOCITY_HASH, _mover.GetNormalizedVelocity());
    }

    internal void TriggerPunch()
    {
        _anim.SetTrigger(PUNCH_HASH);
    }
}
