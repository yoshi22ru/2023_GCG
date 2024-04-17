using Sources.InGame.SecondEdition.Entity;
using Sources.InGame.SecondEdition.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.InGame.SecondEdition.Logic
{
    public class BaseSkillLogic
    {
        private readonly BaseSkillEntity _skill1Entity;
        private readonly InputAction _inputAction;

        public void Tick()
        {
            var input = _inputAction.ReadValue<bool>();

            if (input)
            {
                Debug.Log("Skill1");
            }
        }

        public BaseSkillLogic(BaseSkillEntity skill1Entity, InputAction inputAction)
        {
            _skill1Entity = skill1Entity;
            _inputAction = inputAction;
        }
    }
}