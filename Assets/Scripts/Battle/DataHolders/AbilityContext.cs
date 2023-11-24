using System;
using Core;
using UnityEngine;

namespace DataHolders
{
    [Serializable]
    public class AbilityContext
    {
        public Ability ability;
        public float manaCost;
        public AnimationClip animation;
    }
}