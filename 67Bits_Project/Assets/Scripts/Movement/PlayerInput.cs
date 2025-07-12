using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputActionReference _move = null;
    [SerializeField] InputActionReference _jump = null;

    public Vector2 Move { get => _move.action.ReadValue<Vector2>(); }
    //public bool Jump { get => _jump.action.WasPerformedThisFrame(); }

    //private void Update()
    //{
    //    Debug.Log($"{_move.action.ReadValue<Vector2>()}");
    //}
}
