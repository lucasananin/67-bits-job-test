using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] List<Collider> _areas = null;
    [SerializeField] AIEntity _prefab = null;
    [SerializeField] int _maxCapacity = 5;

    [Header("// READONLY")]
    [SerializeField] List<AIEntity> _poolList = null;
    [SerializeField] List<AIEntity> _activeList = null;

    private void Start()
    {
        PopulatePool(10);
        StartCoroutine(Spawn_Routine());
    }

    private IEnumerator Spawn_Routine()
    {
        while (true)
        {
            while (_activeList.Count >= _maxCapacity)
            {
                yield return null;
            }

            var _area = _areas[Random.Range(0, _areas.Count)];
            var _position = GetRandomPointInBounds(_area.bounds);
            _position.y = 0;
            var _rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            var _entity = GetEntity();
            _entity.transform.SetPositionAndRotation(_position, _rotation);
            _activeList.Add(_entity);

            yield return new WaitForSeconds(.1f);
        }
    }

    public void PopulatePool(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            var _instance = Instantiate(_prefab, Vector3.one * -12345f, Quaternion.identity);
            _poolList.Add(_instance);
        }
    }

    public AIEntity GetEntity()
    {
        int _count = _poolList.Count;

        for (int i = 0; i < _count; i++)
        {
            var _entity = _poolList[i];

            if (!_activeList.Contains(_entity))
            {
                _entity.Init(this);
                return _entity;
            }
        }

        PopulatePool(1);
        var _newEntity = _poolList[^1];
        return _newEntity;
    }

    public void ReturnToPool(AIEntity _entity)
    {
        _activeList.Remove(_entity);
        _entity.transform.position = Vector3.one * -123456f;
    }

    public Vector3 GetRandomPointInBounds(Bounds _bounds)
    {
        return new Vector3(
            Random.Range(_bounds.min.x, _bounds.max.x),
            Random.Range(_bounds.min.y, _bounds.max.y),
            Random.Range(_bounds.min.z, _bounds.max.z)
        );
    }
}
