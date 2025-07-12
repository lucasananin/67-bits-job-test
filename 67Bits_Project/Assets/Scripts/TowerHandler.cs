using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TowerHandler : MonoBehaviour
{
    [Header("// GENERAL")]
    [SerializeField] Transform _root = null;
    [SerializeField] int _capacity = 10;

    [Header("// MOVEMENT")]
    [SerializeField] float _segmentSpacing = 1f;
    [SerializeField] float _followSmoothness = 15f;
    [SerializeField] float _inertia = 15f;
    [SerializeField] float _rotationSmoothness = 1f;

    [Header("// GENERATOR")]
    [SerializeField] List<Transform> _prefabs = null;
    [SerializeField] AIEntity _aiPrefab = null;
    [SerializeField] int _initialCount = 10;

    [Header("// READONLY")]
    [SerializeField] List<Transform> _segments = null;
    [SerializeField] List<NoodleSegmentData> _dataList = null;

    private void Start()
    {
        _segments.Add(_root);
        AddData(_root);

        for (int i = 0; i < _initialCount; i++)
        {
            //AddSegment();
            AddSegmentRB();
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

    [ContextMenu(nameof(AddSegmentRB))]
    public void AddSegmentRB()
    {
        if (_segments.Count >= _capacity + 1) return;
        var _instance = Instantiate(_aiPrefab, Vector3.up * _segments.Count, Quaternion.identity);
        _instance.GoToTower();
        var _handle = _instance.HandleRB.transform;
        _segments.Add(_handle);
        AddData(_handle);
    }

    //[ContextMenu(nameof(RemoveSegmentRB))]
    //public void RemoveSegmentRB()
    //{
    //    if (_segments.Count <= 1) return;
    //    var _instance = _segments[^1];
    //    _segments.Remove(_instance);
    //    _rb.isKinematic = false;
    //    RemoveData();
    //}

    [ContextMenu("AddSegment()")]
    public void AddSegment()
    {
        if (_segments.Count >= _capacity + 1) return;

        var _prefab = _prefabs[Random.Range(0, _prefabs.Count)];
        var _instance = Instantiate(_prefab);
        _segments.Add(_instance);
        AddData(_instance);
    }

    [ContextMenu("RemoveSegment()")]
    public void RemoveSegment()
    {
        if (_segments.Count <= 1) return;

        var _instance = _segments[^1];
        _segments.Remove(_instance);
        Destroy(_instance.gameObject);
        RemoveData();
    }

    private void AddData(Transform _transform)
    {
        _dataList.Add(new NoodleSegmentData
        {
            position = _transform.position,
            rotation = _transform.rotation,
            velocity = Vector3.zero
        });
    }

    private void RemoveData()
    {
        _dataList.RemoveAt(_dataList.Count - 1);
    }
}

[System.Serializable]
public class NoodleSegmentData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
}
