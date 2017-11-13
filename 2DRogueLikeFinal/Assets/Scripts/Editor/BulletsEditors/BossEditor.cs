using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace BulletsGenerator
{
    [CustomEditor(typeof(BulletsAttack))]
    public class BossEditor : 
        EditorWithSubEditors<BulletsWaveEditor,BulletsWave>
    {
        private BulletsAttack BulletsAttack;

        private SerializedProperty bossLocationProperty;
        private SerializedProperty bossProperty;

        private const float bulletsWaveButtonWidth = 125f;

        private const string bossPropName = "bulletsAttack";
        private const string bossPropBulletsWaveName = "bulletsWaveCollected";

        private void OnEnable()
        {
            BulletsAttack = (BulletsAttack)target;

            bossLocationProperty = serializedObject.FindProperty(bossPropName);
            bossProperty = serializedObject.FindProperty(bossPropBulletsWaveName);

            CheckAndCreateSubEditors(BulletsAttack.bulletsWaveCollected);
        }

        private void OnDisable()
        {
            CleanupEditors();
        }

        protected override void SubEditorSetup(BulletsWaveEditor editor)
        {
            editor.bossProperty = bossProperty;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CheckAndCreateSubEditors(BulletsAttack.bulletsWaveCollected);

            EditorGUILayout.PropertyField(bossLocationProperty);

            for (int i = 0; i<subEditors.Length;i++)
            {
                subEditors[i].OnInspectorGUI();
                EditorGUILayout.Space();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Add Collection", GUILayout.Width(bulletsWaveButtonWidth)))
            {
                BulletsWave newBulletsWave = BulletsWaveEditor.CreateBulletsWave();
                bossProperty.AddToObjectArray(newBulletsWave);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}