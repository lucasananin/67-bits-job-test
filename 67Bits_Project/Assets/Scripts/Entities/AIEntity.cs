using System.Collections;
using UnityEngine;

public class AIEntity : MonoBehaviour
{
    [SerializeField] Rigidbody _handleRB = null;
    [SerializeField] Collider _collider = null;
    [SerializeField] AIAnimator _anim = null;
    [SerializeField] float _knockdownDuration = 5f;
    [SerializeField] Vector2Int _priceRange = new(3, 6);

    private NPCSpawner _spawner = null;
    private TowerHandler _tower = null;

    public Rigidbody HandleRB { get => _handleRB; }

    internal void Init(NPCSpawner _spawner, TowerHandler _towerHandler)
    {
        this._spawner = _spawner;
        _tower = _towerHandler;
        _collider.enabled = true;
    }

    public void Knockdown(Vector3 _origin, float _force, float _radius, float _upwardsMultiplier)
    {
        StartCoroutine(Knockdown_Routine(_origin, _force, _radius, _upwardsMultiplier));
    }

    private IEnumerator Knockdown_Routine(Vector3 _origin, float _force, float _radius, float _upwardsMultiplier)
    {
        _collider.enabled = false;
        _anim.EnableRagdoll();
        _anim.Explode(_origin, _force, _radius, _upwardsMultiplier);

        yield return new WaitForSeconds(_knockdownDuration);

        GoToTower();
    }

    public void GoToTower()
    {
        if (_tower.IsFull())
        {
            Die();
        }
        else
        {
            _handleRB.isKinematic = true;
            _tower.AddSegment(_handleRB.transform);
        }
    }

    internal void Die()
    {
        _spawner.ReturnToPool(this);
        _anim.DisableRagdoll();
    }

    internal int GetCostValue()
    {
        return Random.Range(_priceRange.x, _priceRange.y + 1);
    }
}
