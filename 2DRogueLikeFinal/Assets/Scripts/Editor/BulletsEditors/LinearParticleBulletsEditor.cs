using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BulletsGenerator
{
    [CustomEditor(typeof(LinearParticleBullets))]
    public class LinearBulletsEditor : BulletsEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Linear Particle Bullets";
        }
    }
}