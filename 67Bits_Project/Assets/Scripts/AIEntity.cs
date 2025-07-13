using System.Collections;
using UnityEngine;

public class AIEntity : MonoBehaviour
{
    [SerializeField] Rigidbody _handleRB = null;
    [SerializeField] Collider _collider = null;
    [SerializeField] AIAnimator _anim = null;

    public Rigidbody HandleRB { get => _handleRB; }

    public void GoToTower()
    {
        _handleRB.isKinematic = true;
    }

    public void Knockdown(Vector3 _origin, float _force, float _radius, float _upwardsMultiplier)
    {
        //_collider.enabled = false;
        //_anim.Explode(_origin, _force);
        StartCoroutine(GetUp_Routine(_origin, _force, _radius, _upwardsMultiplier));
    }

    private IEnumerator GetUp_Routine(Vector3 _origin, float _force, float _radius, float _upwardsMultiplier)
    {
        _collider.enabled = false;
        _anim.EnableRagdoll();
        _anim.Explode(_origin, _force, _radius, _upwardsMultiplier);

        yield return new WaitForSeconds(5);

        _anim.DisableRagdoll();
        _collider.enabled = true;
    }
}
