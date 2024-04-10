using System.Threading;
using R3;
using UnityEngine;

namespace Sources.InGame.BattleObject
{
    public record BuffEntity
    {
        public readonly BuffType Type;
        public readonly float BuffValue;
        public readonly float BuffLength;
        private float _remainTime;
        private readonly IRequestDispose<BuffEntity> _owner;
        
        public BuffEntity(BuffType type, float value, float length, IRequestDispose<BuffEntity> owner)
        {
            Type = type;
            BuffValue = value;
            BuffLength = length;
            _remainTime = length;
            _owner = owner;
        }

        public void Tick()
        {
            _remainTime -= Time.deltaTime;
            if (_remainTime <= 0f)
            {
                _owner.RequestDispose(this);
            }
        }

        public float Amount()
        {
            return _remainTime / BuffLength;
        }
    }
}