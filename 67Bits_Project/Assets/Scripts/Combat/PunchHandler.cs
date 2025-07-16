using UnityEngine;
using UnityEngine.Events;

public class PunchHandler : MonoBehaviour
{
    [SerializeField] Transform _muzzle = null;
    [SerializeField] SphereCollider _collider = null;
    [SerializeField] LayerMask _layerMask = default;
    [SerializeField] float _force = 50f;
    [SerializeField] float _radius = 1f;
    [SerializeField] float _upwardsMultiplier = 1f;

    private readonly RaycastHit[] _results = new RaycastHit[9];

    public event UnityAction OnPunch = null;

    private void FixedUpdate()
    {
        var _hits = Physics.SphereCastNonAlloc(_muzzle.position, _collider.radius, transform.forward, _results, 0, _layerMask);

        for (int i = 0; i < _hits; i++)
        {
            var _colliderHit = _results[i].collider;

            if (_colliderHit.TryGetComponent(out AIEntity _entity))
            {
                _entity.Knockdown(transform.position, _force, _radius, _upwardsMultiplier);
            }
        }

        if (_hits > 0)
        {
            OnPunch?.Invoke();
        }
    }
}
