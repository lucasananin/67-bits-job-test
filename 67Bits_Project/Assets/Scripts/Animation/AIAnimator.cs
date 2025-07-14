using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour
{
    [SerializeField] Animator _anim = null;
    [SerializeField] Transform _hipBone = null;
    [SerializeField] Rigidbody _handleRb = null;
    [SerializeField] List<Rigidbody> _rigidbodies = null;
    [SerializeField] List<Collider> _colliders = null;

    private void Awake()
    {
        ToggleRagdoll(false);
    }

    [ContextMenu("Fodase")]
    public void Fodase()
    {
        var _a = _hipBone.GetComponentsInChildren<Rigidbody>();
        var _b = _hipBone.GetComponentsInChildren<Collider>();
        _rigidbodies = new List<Rigidbody>(_a);
        _colliders = new List<Collider>(_b);
    }

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
        _handleRb.isKinematic = false;
        AlignPositionToHips();
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool _value)
    {
        int _count = _rigidbodies.Count;

        for (int i = 0; i < _count; i++)
        {
            _anim.enabled = !_value;
            _rigidbodies[i].isKinematic = !_value;
            _colliders[i].enabled = _value;
        }
    }

    private void AlignPositionToHips()
    {
        var _originalHipsPosition = _hipBone.position;

        var _worldPosition = _hipBone.position;
        _worldPosition.y = 0;
        transform.position = _worldPosition;

        _hipBone.position = _originalHipsPosition;
    }
}
