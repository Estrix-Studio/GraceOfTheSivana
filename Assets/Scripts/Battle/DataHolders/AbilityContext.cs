using System;
using Battle.Core;
using UnityEngine;

namespace Battle.DataHolders
{
    [Serializable]
    public class AbilityContext
    {
        public Ability ability;
        public float manaCost;
        public AnimationClip animation;
    }
}