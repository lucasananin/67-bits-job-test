using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputActionReference _moveReference = null;
    [SerializeField] bool _useVirtualControls = false;
    //[SerializeField] InputActionReference _jump = null;

    [Header("// READONLY")]
    [SerializeField] Vector2 _virtualInput = default;

    public Vector2 Move { get => GetMoveValue(); }
    //public bool Jump { get => _jump.action.WasPerformedThisFrame(); }

    //private void Update()
    //{
    //    Debug.Log($"{Move}");
    //}

    public void ApplyVirtualMove(Vector2 _value)
    {
        Vector2 _direction = _value.normalized;
        float _clampedMagnitude = Mathf.Clamp(_value.magnitude, 0f, 1f);
        _virtualInput = _direction * _clampedMagnitude;

    }

    public Vector2 GetMoveValue()
    {
        if (_useVirtualControls)
        {
            return _virtualInput;
        }
        else
        {
            return _moveReference.action.ReadValue<Vector2>();
        }
    }
}
