using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour
{
    [SerializeField] Animator _anim = null;
    [SerializeField] Transform _hipBone = null;
    [SerializeField] Rigidbody _handleRb = null;
    [SerializeField] List<Rigidbody> _rigidbodies = null;
    [SerializeField] List<Collider> _colliders = null;

    private Vector3[] storedPositions;
    private Quaternion[] storedRotations;

    private void Awake()
    {
        storedPositions = new Vector3[_rigidbodies.Count];
        storedRotations = new Quaternion[_colliders.Count];
        ToggleRagdoll(false);
    }

    //private void Start()
    //{

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
        //int _count = _rigidbodies.Count;
        //for (int i = 0; i < _count; i++)
        //{
        //    storedPositions[i] = _rigidbodies[i].transform.localPosition;
        //    storedRotations[i] = _rigidbodies[i].transform.localRotation;
        //}
        //_anim.enabled = false;

        ToggleRagdoll(true);
    }

    [ContextMenu(nameof(DisableRagdoll))]
    public void DisableRagdoll()
    {
        //var _worldPosition = _handleRb.position;
        //_worldPosition.y = 0;
        //transform.position = _worldPosition;
        _handleRb.isKinematic = false;
        AlignPositionToHips();
        ToggleRagdoll(false);

        //int _count = _rigidbodies.Count;
        //for (int i = 0; i < _count; i++)
        //{
        //    _rigidbodies[i].transform.SetLocalPositionAndRotation(storedPositions[i], storedRotations[i]);
        //}
        //_anim.enabled = true;
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
