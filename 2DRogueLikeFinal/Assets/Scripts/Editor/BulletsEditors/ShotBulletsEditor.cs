using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BulletsGenerator
{
    [CustomEditor(typeof(ShotParticleBullets))]
    public class ShotParticleBulletsEditor : BulletsEditor
    {
        protected override string GetFoldoutLabel()
        {
            return "Shot Particle Bullets";
        }
    }
}