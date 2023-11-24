using System;
using Core;
using UnityEngine;

namespace DataHolders
{
    /// <summary>
    /// Used to connect ability with animation in battle.
    /// </summary>
    [Serializable]
    public class AnimatedAbility
    {
        public Ability ability;
        public AnimationClip animation;
    }
}
