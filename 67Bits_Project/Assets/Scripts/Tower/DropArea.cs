using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class DropArea : OnTriggerEvent
{
    [SerializeField] float _jumpPower = 2f;
    [SerializeField] float _moveDuration = 1f;
    [SerializeField] float _grabInterval = 1f;

    private WaitForSeconds _waitTime = null;

    public static event UnityAction<AIEntity> OnDelivered = null;

    private void Awake()
    {
        _waitTime = new WaitForSeconds(_grabInterval);
    }

    public override void OnEnterMethod(Collider _other)
    {
        if (_other.TryGetComponent(out TowerHandler _tower))
        {
            var _segments = _tower.PopSegments();

            if (_segments != null)
                StartCoroutine(MoveSegments_Routine(_segments));
        }

        base.OnEnterMethod(_other);
    }

    private IEnumerator MoveSegments_Routine(List<Transform> _segments)
    {
        int _count = _segments.Count;

        for (int i = _count - 1; i >= 0; i--)
        {
            var _segment = _segments[i];
            _segment.DOJump(transform.position, _segment.position.y + _jumpPower, 1, _moveDuration).
                OnComplete(() =>
                {
                    var _entity = _segment.GetComponentInParent<AIEntity>();
                    _entity.Die();
                    OnDelivered?.Invoke(_entity);
                });

            yield return _waitTime;
        }
    }
}
