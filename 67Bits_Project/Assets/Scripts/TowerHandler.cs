using System.Collections.Generic;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [SerializeField] Transform _root = null;
    [SerializeField] float _segmentSpacing = 1f;
    [SerializeField] float _followSmoothness = 15f;
    [SerializeField] float _inertia = 15f;
    [SerializeField] float _rotationSmoothness = 1f;

    [Header("// GENERATOR")]
    [SerializeField] List<Transform> _prefabs = null;
    [SerializeField] int _initialCount = 10;

    [Header("// READONLY")]
    [SerializeField] List<Transform> _segments;

    private readonly List<NoodleSegmentData> _dataList = new();

    private void Start()
    {
        _segments.Add(_root);

        for (int i = 0; i < _initialCount; i++)
        {
            var _prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            var _instance = Instantiate(_prefab);
            _segments.Add(_instance);
        }

        int _count = _segments.Count;

        for (int i = 0; i < _count; i++)
        {
            _dataList.Add(new NoodleSegmentData
            {
                position = _segments[i].position,
                rotation = _segments[i].rotation,
                velocity = Vector3.zero
            });
        }
    }

    private void LateUpdate()
    {
        MoveSegments();
    }

    private void MoveSegments()
    {
        for (int i = 0; i < _segments.Count; i++)
        {
            var _segment = _segments[i];
            var _data = _dataList[i];

            if (i == 0)
            {
                _data.position = _segments[0].position;
                _data.rotation = _segments[0].rotation;
            }
            else
            {
                var _previousData = _dataList[i - 1];

                Vector3 _targetPosition = _previousData.position + Vector3.up * _segmentSpacing;
                Vector3 _desiredVelocity = (_targetPosition - _data.position) * _followSmoothness;
                _data.velocity = Vector3.Lerp(_data.velocity, _desiredVelocity, Time.deltaTime * _inertia);
                _data.position += _data.velocity * Time.deltaTime;

                Vector3 _direction = _previousData.position - _data.position;

                if (_direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(_direction);
                    _data.rotation = Quaternion.Slerp(_data.rotation, targetRot, Time.deltaTime * _rotationSmoothness);
                }
            }

            _segment.SetPositionAndRotation(_data.position, _data.rotation);
        }
    }
}

public class NoodleSegmentData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
}
