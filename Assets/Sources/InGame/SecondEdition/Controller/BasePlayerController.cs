using Sources.InGame.SecondEdition.Entity;
using Sources.InGame.SecondEdition.Logic;

namespace Sources.InGame.SecondEdition.Controller
{
    public class BasePlayerController
    {
        private readonly PlayerMoveLogic _playerMoveLogic;
        private readonly BaseSkillLogic _baseSkillLogic;

        public BasePlayerController(PlayerMoveLogic playerMoveLogic, BaseSkillLogic skill1Logic)
        {
            _playerMoveLogic = playerMoveLogic;
            _baseSkillLogic = skill1Logic;
        }

        public void Tick()
        {
            _playerMoveLogic.Tick();
            _baseSkillLogic.Tick();
        }
    }
}