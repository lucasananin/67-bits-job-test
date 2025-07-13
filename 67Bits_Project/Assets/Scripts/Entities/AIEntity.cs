using System.Collections;
using UnityEngine;

public class AIEntity : MonoBehaviour
{
    [SerializeField] Rigidbody _handleRB = null;
    [SerializeField] Collider _collider = null;
    [SerializeField] AIAnimator _anim = null;
    [SerializeField] float _knockdownDuration = 5f;

    public Rigidbody HandleRB { get => _handleRB; }

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

        //_anim.DisableRagdoll();
        //_collider.enabled = true;
        GoToTower();
    }

    public void GoToTower()
    {
        _handleRB.isKinematic = true;
        var _tower = FindFirstObjectByType<TowerHandler>();
        _tower.AddSegment(_handleRB.transform);
    }
}
