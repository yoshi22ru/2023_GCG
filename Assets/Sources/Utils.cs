using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    }
}
