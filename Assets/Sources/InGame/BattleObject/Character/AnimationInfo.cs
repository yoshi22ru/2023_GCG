
using UnityEngine;

namespace Sources.InGame.BattleObject.Character
{
    public struct AnimationInfo
    {
        public int Id { get; private set; }
        public float Length { get; private set; }

        public AnimationInfo(string name, Animator animator)
        {
            Id = Animator.StringToHash(name);
            Length = Utils.GetAnimationClipLength(animator.runtimeAnimatorController.animationClips, name);
        }
    }
}