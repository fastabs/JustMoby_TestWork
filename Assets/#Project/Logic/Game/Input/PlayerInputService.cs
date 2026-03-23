using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class PlayerInputService : IInitializable, ITickable, System.IDisposable
    {
        private readonly InputActions _inputActions;
        private readonly ICursorService _cursorService;
        private readonly IPlayerMovementService _playerMovementService;

        public PlayerInputService(InputActions inputActions,
            ICursorService cursorService,
            IPlayerMovementService playerMovementService)
        {
            _inputActions = inputActions;
            _cursorService = cursorService;
            _playerMovementService = playerMovementService;
        }

        public void Initialize()
        {
            _inputActions.Enable();
            _inputActions.Player.Fire.performed += OnFirePerformed;
            _inputActions.Player.CursorMode.performed += OnCursorModePerformed;

            _cursorService.Hide();
        }

        public void Dispose()
        {
            _inputActions.Player.Fire.performed -= OnFirePerformed;
            _inputActions.Player.CursorMode.performed -= OnCursorModePerformed;
            _inputActions.Disable();
            _inputActions.Dispose();
        }

        public void Tick()
        {
            var move = _inputActions.Player.Move.ReadValue<Vector2>();
            _playerMovementService.Move(move);

            if (_cursorService.CursorMode == CursorMode.Hidden)
            {
                var rotate = _inputActions.Player.Rotation.ReadValue<Vector2>();
                _playerMovementService.Rotate(rotate);
            }
        }

        private void OnFirePerformed(InputAction.CallbackContext _)
        {
            Debug.Log("Fire");
        }

        private void OnCursorModePerformed(InputAction.CallbackContext _)
        {
            _cursorService.Toggle();
        }
    }
}
