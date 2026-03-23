using UnityEngine;

namespace JustMoby_TestWork
{
    public interface IPlayerMovementService
    {
        void Move(Vector2 delta);
        void Rotate(Vector2 delta);
    }

    public sealed class PlayerMovementService : IPlayerMovementService
    {
        private readonly GameConfiguration _configuration;
        private readonly Transform _transform;

        private Vector2 _rotation;

        public PlayerMovementService(Transform transform, GameConfiguration configuration)
        {
            _transform = transform;
            _configuration = configuration;

            _rotation = _transform.localEulerAngles;
        }

        public void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01)
                return;

            var moveSpeed = _configuration.Player.MoveSpeed;
            var scaledMoveSpeed = moveSpeed * Time.deltaTime;

            var move = Quaternion.Euler(0, _transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
            _transform.position += move * scaledMoveSpeed;
        }

        public  void Rotate(Vector2 rotate)
        {
            if (rotate.sqrMagnitude < 0.01)
                return;

            var rotationSpeed = _configuration.Player.RotationSpeed;
            var scaledRotateSpeed = rotationSpeed * Time.deltaTime;
            _rotation.y += rotate.x * scaledRotateSpeed;
            _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
            _transform.localEulerAngles = _rotation;
        }
    }
}