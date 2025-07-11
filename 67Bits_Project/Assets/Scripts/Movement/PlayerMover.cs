using UnityEngine;

public class PlayerMover : AbstractMover
{
    [SerializeField] PlayerInput _input = null;
    [SerializeField] CharacterController _cc = null;
    [SerializeField] float _speed = 2.0f;
    //[SerializeField] float _jumpHeight = 1.0f;
    [SerializeField] float _gravityValue = -9.81f;

    [Header("// READONLY")]
    [SerializeField] Vector3 _playerVelocity;
    [SerializeField] bool _isGrounded;

    private void Update()
    {
        _isGrounded = _cc.isGrounded;

        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        Vector3 _move = new(_input.Move.x, 0, _input.Move.y);

        //if (_input.Jump && _isGrounded)
        //{
        //    _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        //}

        _playerVelocity.y += _gravityValue * Time.deltaTime;

        Vector3 _motion = (_move * _speed) + (_playerVelocity.y * Vector3.up);
        _cc.Move(_motion * Time.deltaTime);
    }

    public override float GetNormalizedVelocity()
    {
        return Mathf.InverseLerp(0, _speed, _cc.velocity.magnitude);
    }
}
