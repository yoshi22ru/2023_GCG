using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.InGame.BattleObject.Character
{
    public class BuffManager
    {
        private int _currentBuff;
        private readonly Dictionary<BuffType, CancellationTokenSource> _cancellationTokens;

        public BuffManager()
        {
            _currentBuff = 0;
            _cancellationTokens = new Dictionary<BuffType, CancellationTokenSource>();
        }
        
        protected void SetBuff(BuffType buffType)
        {
            _currentBuff |= (int)buffType;
        }

        protected async UniTask SetBuffWithTimer(BuffType buffType, float exitTime)
        {
            if (HasBuff(buffType))
            {
                // todo RemoveTimer
                var result = _cancellationTokens.TryGetValue(buffType, out var tokenSource);
                if (!result)
                {
                    Debug.Log($"No Token in {buffType}");
                    return;
                }
                tokenSource.Cancel();
                _cancellationTokens.Remove(buffType);
            }

            _currentBuff |= (int)buffType;
            await RemoveBuffWithTimer(buffType, exitTime);
        }

        protected void RemoveBuff(BuffType buffType)
        {
            _currentBuff &= ~(int)buffType;
        }

        private async UniTask RemoveBuffWithTimer(BuffType buffType, float exitTime)
        {
            // FIXME : Init CancellationTokenSource In Constructor
            var cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokens.Add(buffType, cancellationTokenSource);
            var token = cancellationTokenSource.Token;
            await UniTask.Delay((int)(exitTime * 1000),cancellationToken:token).SuppressCancellationThrow();
            RemoveBuff(buffType);
        }

        public bool HasBuff(BuffType buffType)
        {
            return (_currentBuff & (int)buffType) != 0;
        }
        
        private struct BuffDetail
        {
            public float Value { get; private set; }
            public CancellationTokenSource TokenSource { get; private set; }

            public BuffDetail(float value, CancellationTokenSource tokenSource)
            {
                Value = value;
                TokenSource = tokenSource;
            }
        }
    }
}