using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.InGame.BattleObject.Character
{
    public class BuffManager: IRequestDispose<BuffEntity>
    {
        private readonly List<BuffEntity> _buffEntities =new ();

        public void Tick()
        {
            foreach (var entity in _buffEntities)
            {
                entity.Tick();
            }
        }

        public float GetBuffSum(BuffType buffType)
        {
            return _buffEntities.Where(entity => entity.Type == buffType)
                .Sum(entity => entity.BuffValue);
        }
        
        public void SetBuff(BuffType buffType, float value, float exitTime)
        {
            Debug.Log("Called");
            _buffEntities.Add(new  BuffEntity(buffType, value, exitTime, this));
        }

        public void ClearBuff(BuffType buffType)
        {
            _buffEntities.Clear();
        }

        public bool HasBuff(BuffType buffType)
        {
            return _buffEntities.Any(entity => buffType == entity.Type);
        }

        public void RequestDispose(BuffEntity toDispose)
        {
            _buffEntities.Remove(toDispose);
        }
    }
}