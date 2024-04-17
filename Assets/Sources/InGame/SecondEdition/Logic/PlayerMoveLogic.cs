using Sources.InGame.SecondEdition.Entity;
using Sources.InGame.SecondEdition.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.InGame.SecondEdition.Logic
{
    public class PlayerMoveLogic
    {
        private readonly PlayerMoveEntity _moveEntity;
        private readonly PlayerView _playerView;
        private readonly InputAction _inputAction;

        public void Tick()
        {
            var value = _inputAction.ReadValue<Vector2>();
            Vector3 input = new(value.x, 0, value.y);

            _playerView.ModelRigidbody.velocity = input * _moveEntity.BaseSpeed;
        }

        public PlayerMoveLogic(PlayerMoveEntity playerMoveEntity, PlayerView playerView, InputAction inputAction)
        {
            _moveEntity = playerMoveEntity;
            _playerView = playerView;
            _inputAction = inputAction;
        }
    }
}