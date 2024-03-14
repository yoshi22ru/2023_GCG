using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.InGame.BattleObject;
using UnityEngine;

namespace Sources
{

    public static class Utils
    {
        public static float GetAnimationClipLength(IEnumerable<AnimationClip> animationClips, string clipName)
        {
            return animationClips
                .Where(animationClip => animationClip.name == clipName)
                .Select(clip => clip.length)
                .FirstOrDefault();
        }

        public static string FormatBattleObjectInformation(GameObject gameObject, BattleObject battleObject)
        {
            return $"\t{gameObject.name}\n" +
                   $"\t{battleObject.GetTeam()}\n";
        }
    }
}
