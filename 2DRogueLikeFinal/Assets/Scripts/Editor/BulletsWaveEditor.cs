using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace BulletsGenerator
{
    [CustomEditor(typeof(BulletsWave))]
    public class BulletsWaveEditor : EditorWithSubEditors<BulletsEditor,Bullets>
    {

        public SerializedProperty bossProperty;

        
        private BulletsWave bulletsWave;
        private SerializedProperty bulletsProperty;
        private SerializedProperty descriptionProperty;

        private Type[] bulletsTypes;
        private string[] bulletsTypeNames;
        private int selectedIndex;

        private const float waveButtonWidth = 125f;
        private const float dropAreaHeight = 60f;
        private const float controlSpacing = 5f;

        private const string bulletsWavePropDescriptionName = "description";
        private const string bulletsPropName = "bullets";

        private readonly float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;

        private void OnEnable()
        {
            bulletsWave = (BulletsWave)target;

            descriptionProperty = serializedObject.FindProperty(bulletsWavePropDescriptionName);

            bulletsProperty = serializedObject.FindProperty(bulletsPropName);

            CheckAndCreateSubEditors(bulletsWave.bullets);

            SetBulletsNamesArray();
        }

        private void OnDisable()
        {
            CleanupEditors();
        }

        protected override void SubEditorSetup(BulletsEditor editor)
        {
            editor.bulletsProperty = bulletsProperty;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            CheckAndCreateSubEditors(bulletsWave.bullets);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();

            descriptionProperty.isExpanded = EditorGUILayout.Foldout(descriptionProperty.isExpanded, descriptionProperty.stringValue);

            if (GUILayout.Button("Remove Wave",GUILayout.Width(waveButtonWidth)))
            {
                bossProperty.RemoveFromObjectArray(bulletsWave);
            }

            EditorGUILayout.EndHorizontal();

            if (descriptionProperty.isExpanded)
                ExpandedGUI();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        private void ExpandedGUI()
        {
            for (int i = 0; i < subEditors.Length; i++)
                subEditors[i].OnInspectorGUI();

            if (bulletsWave.bullets.Length > 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.Space();
            }

            Rect fullWidthRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(dropAreaHeight + verticalSpacing));

            Rect leftAreaRect = fullWidthRect;

            leftAreaRect.y += verticalSpacing * 0.5f;

            leftAreaRect.width *= 0.5f;
            leftAreaRect.width -= controlSpacing * 0.5f;

            leftAreaRect.height = dropAreaHeight;

            Rect rightAreaRect = leftAreaRect;

            rightAreaRect.x += rightAreaRect.width + controlSpacing;

            TypeSelectionGui(leftAreaRect);

            DragAndDropAreaGUI(rightAreaRect);

            DraggingAndDropping(rightAreaRect, this);            
        }

        private void TypeSelectionGui(Rect containingRect)
        {
            Rect topHalf = containingRect;

            topHalf.height *= 0.5f;

            Rect bottomHalf = topHalf;
            bottomHalf.y += bottomHalf.height;

            selectedIndex = EditorGUI.Popup(topHalf, selectedIndex, bulletsTypeNames);

            if (GUI.Button(bottomHalf,"Add Selected Bullets"))
            {
                Type bulletsType = bulletsTypes[selectedIndex];
                Bullets newBullets = BulletsEditor.CreateBullets(bulletsType);
                bulletsProperty.AddToObjectArray(newBullets);
            }
        }

        private static void DragAndDropAreaGUI(Rect containingRect)
        {
            GUIStyle centredStyle = GUI.skin.box;

            centredStyle.alignment = TextAnchor.MiddleCenter;
            centredStyle.normal.textColor = GUI.skin.button.normal.textColor;

            GUI.Box(containingRect, "Drop new Reactions here", centredStyle);
        }

        private static void DraggingAndDropping (Rect dropArea, BulletsWaveEditor editor)
        {
            Event currentEvent = Event.current;

            
            if (!dropArea.Contains(currentEvent.mousePosition))
                return;

            switch (currentEvent.type)
            {
                case EventType.DragUpdated:

                    DragAndDrop.visualMode = IsDragValid() ? DragAndDropVisualMode.Link : DragAndDropVisualMode.Rejected;

                    currentEvent.Use();

                    break;

                case EventType.DragPerform:

                    DragAndDrop.AcceptDrag();

                    for (int i = 0; i< DragAndDrop.objectReferences.Length;i++)
                    {
                        MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;

                        Type bulletsType = script.GetType();

                        Bullets newBullets = BulletsEditor.CreateBullets(bulletsType);
                        editor.bulletsProperty.AddToObjectArray(newBullets);
                    }

                    currentEvent.Use();

                    break;
            }
        }

        private static bool IsDragValid()
        {
            for (int i = 0; i<DragAndDrop.objectReferences.Length;i++)
            {
                if (DragAndDrop.objectReferences[i].GetType() != typeof(MonoScript))
                    return false;

                MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
                Type scriptType = script.GetClass();

                if (!scriptType.IsSubclassOf(typeof(Bullets)))
                    return false;

                if (scriptType.IsAbstract)
                    return false;
            }

            return false;
        }

        private void SetBulletsNamesArray()
        {
            Type bulletsType = typeof(Bullets);

            Type[] allTypes = bulletsType.Assembly.GetTypes();

            List<Type> bulletsSubTypeList = new List<Type>();

            for (int i = 0;i<allTypes.Length;i++)
            {
                if (allTypes[i].IsSubclassOf(bulletsType) && !allTypes[i].IsAbstract)
                {
                    bulletsSubTypeList.Add(allTypes[i]);
                }
            }

            bulletsTypes = bulletsSubTypeList.ToArray();

            List<string> bulletsTypeNameList = new List<string>();

            for (int i = 0;i<bulletsTypes.Length;i++)
            {
                bulletsTypeNameList.Add(bulletsTypes[i].Name);
            }

            bulletsTypeNames = bulletsTypeNameList.ToArray();
        }

        public static BulletsWave CreateBulletsWave()
        {
            BulletsWave newBulletsWave = CreateInstance<BulletsWave>();

            newBulletsWave.description = "New BulletsWave";

            newBulletsWave.bullets = new Bullets[0];
            return newBulletsWave;
        }
    }
}