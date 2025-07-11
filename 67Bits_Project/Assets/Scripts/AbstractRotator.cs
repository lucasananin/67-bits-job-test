using UnityEngine;

public abstract class AbstractRotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed = 10f;

    public void RotateToMovement(Vector3 _velocity)
    {
        var _forward = new Vector3(_velocity.x, 0, _velocity.z).normalized;

        if (_forward != Vector3.zero)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
        }

        //if (_agent.velocity.sqrMagnitude > _minRotationMagnitude)
        //{
        //    var _forward = new Vector3(_agent.velocity.x, 0, _agent.velocity.z).normalized;

        //    if (_forward != Vector3.zero)
        //    {
        //        Quaternion _targetRotation = Quaternion.LookRotation(_forward);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
        //    }
        //}
    }
}
