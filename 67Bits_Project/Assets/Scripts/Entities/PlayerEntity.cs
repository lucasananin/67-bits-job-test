using System;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] PlayerMover _mover = null;
    [SerializeField] PlayerAnimator _anim = null;
    [SerializeField] PunchHandler _punchHandler = null;
    //[SerializeField] TowerHandler _towerHandler = null;
    [SerializeField] float _punchDuration = 1f;

    private void OnEnable()
    {
        _punchHandler.OnPunch += EnterPunchState;
    }

    private void OnDisable()
    {
        _punchHandler.OnPunch -= EnterPunchState;
    }

    public void EnterPunchState()
    {
        _mover.CanMove = false;
        _anim.TriggerPunch();
        Invoke(nameof(ExitPunchState), _punchDuration);
    }

    public void ExitPunchState()
    {
        _mover.CanMove = true;
    }

    //internal bool CanPunch()
    //{
    //    return !_towerHandler.IsFull();
    //}
}
