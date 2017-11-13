using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BulletsGenerator
{
    [CustomEditor(typeof(RandomChasingParticleBullets))]
    public class RandomChasingBulletsEditor : BulletsEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Random Chasing Bullets";
        }
    }
}
