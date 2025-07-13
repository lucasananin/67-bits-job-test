using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    [SerializeField] float _jumpPower = 2f;
    [SerializeField] float _moveDuration = 1f;
    [SerializeField] float _grabInterval = 1f;

    private WaitForSeconds _waitTime = null;

    private void Awake()
    {
        _waitTime = new WaitForSeconds(_grabInterval);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.TryGetComponent(out TowerHandler _tower))
        {
            var _segments = _tower.PopSegments();

            if (_segments != null)
                StartCoroutine(MoveSegments_Routine(_segments));
        }
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
                    // add money.
                    // return segments to pool.
                });

            yield return _waitTime;
        }
    }
}
