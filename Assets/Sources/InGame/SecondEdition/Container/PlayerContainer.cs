using System;
using Sources.InGame.SecondEdition.Controller;
using Sources.InGame.SecondEdition.Entity;
using Sources.InGame.SecondEdition.Logic;
using Sources.InGame.SecondEdition.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.InGame.SecondEdition.Container
{
    public class PlayerContainer: MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;

        private BasePlayerController _basePlayerController;
        private void Awake()
        {
            var playerMoveEntity = new PlayerMoveEntity(10);
            var playerSkill1Entity = new BaseSkillEntity(10, null);
            var playerInput = GetComponent<PlayerInput>();

            var playerMoveLogic = new PlayerMoveLogic(playerMoveEntity, playerView, playerInput.actions["Move"]);
            var playerSkill1Logic = new BaseSkillLogic(playerSkill1Entity, playerInput.actions["Skill1"]);

            _basePlayerController = new BasePlayerController(playerMoveLogic, playerSkill1Logic);
        }

        private void Update()
        {
            _basePlayerController.Tick();
        }
    }
}