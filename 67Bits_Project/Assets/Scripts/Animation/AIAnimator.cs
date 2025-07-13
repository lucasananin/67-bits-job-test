using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour
{
    [SerializeField] Animator _anim = null;
    [SerializeField] Rigidbody _handleRb = null;
    [SerializeField] List<Rigidbody> _rigidbodies = null;
    [SerializeField] List<Collider> _colliders = null;

    private void Awake()
    {
        DisableRagdoll();
    }

    public float _explosionForce = 1f;

    //[ContextMenu(nameof(Explode))]
    //public void Explode()
    //{
    //    EnableRagdoll();
    //    int _count = _rigidbodies.Count;

    //    for (int i = 0; i < _count; i++)
    //    {
    //        _rigidbodies[i].AddExplosionForce(_explosionForce, transform.position + transform.forward, 2f, 1f, ForceMode.Impulse);
    //    }

    //    Invoke(nameof(DisableRagdoll), 5f);
    //}

    public void Explode(Vector3 _origin, float _force, float _radius, float _upwardsMultiplier)
    {
        int _count = _rigidbodies.Count;

        for (int i = 0; i < _count; i++)
        {
            _rigidbodies[i].AddExplosionForce(_force, _origin, _radius, _upwardsMultiplier, ForceMode.Impulse);
        }
    }

    [ContextMenu(nameof(EnableRagdoll))]
    public void EnableRagdoll()
    {
        ToggleRagdoll(true);
    }

    [ContextMenu(nameof(DisableRagdoll))]
    public void DisableRagdoll()
    {
        var _worldPosition = _handleRb.position;
        _worldPosition.y = 0;
        transform.position = _worldPosition;

        _handleRb.isKinematic = false;
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool _value)
    {
        int _count = _rigidbodies.Count;

        for (int i = 0; i < _count; i++)
        {
            _rigidbodies[i].isKinematic = !_value;
            _colliders[i].enabled = _value;
            _anim.enabled = !_value;
        }
    }
}
