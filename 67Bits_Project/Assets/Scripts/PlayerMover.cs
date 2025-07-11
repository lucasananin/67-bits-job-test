using UnityEngine;

public class PlayerMover : AbstractMover
{
    [SerializeField] PlayerInput _input = null;
    [SerializeField] CharacterController _cc = null;
    [SerializeField] float _speed = 2.0f;
    [SerializeField] float _jumpHeight = 1.0f;
    [SerializeField] float _gravityValue = -9.81f;

    [Header("// READONLY")]
    [SerializeField] Vector3 _playerVelocity;
    [SerializeField] bool _isGrounded;

    void Update()
    {
        _isGrounded = _cc.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        //// Horizontal input
        //Vector3 _move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //_move = Vector3.ClampMagnitude(_move, 1f); // Optional: prevents faster diagonal movement

        //var _moveInput = _input.Move;
        Vector3 _move = new(_input.Move.x, 0, _input.Move.y);
        //_move = Vector3.ClampMagnitude(_move, 1f); // Optional: prevents faster diagonal movement

        //if (_move != Vector3.zero)
        //{
        //    transform.forward = _move;
        //}

        // Jump
        //if (Input.GetButtonDown("Jump") && _isGrounded)
        if (_input.Jump && _isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        // Apply gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 _motion = (_move * _speed) + (_playerVelocity.y * Vector3.up);
        _cc.Move(_motion * Time.deltaTime);
    }
}
