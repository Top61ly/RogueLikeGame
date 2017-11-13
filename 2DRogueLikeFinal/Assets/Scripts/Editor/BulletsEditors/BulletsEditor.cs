using System;
using UnityEngine;
using UnityEditor;

namespace BulletsGenerator
{

    public abstract class BulletsEditor : Editor
    {
        public bool showBullets;
        public SerializedProperty bulletsProperty;

        private Bullets bullets;

        private const float buttonWidth = 30f;

        private void OnEnable()
        {
            bullets = (Bullets)target;
        }

        protected virtual void Init()
        { }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();

            showBullets = EditorGUILayout.Foldout(showBullets, GetFoldoutLabel());

            if (GUILayout.Button("-",GUILayout.Width(buttonWidth)))
            {
                bulletsProperty.RemoveFromObjectArray(bullets);
            }

            EditorGUILayout.EndHorizontal();

            if (showBullets)
                DrawBullets();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }
        
        public static Bullets CreateBullets(Type bulletsType)
        {
            return (Bullets)CreateInstance(bulletsType);
        }

        protected virtual void DrawBullets()
        {
            DrawDefaultInspector();
        }

        protected abstract string GetFoldoutLabel();
    }
}