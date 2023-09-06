using UnityEngine;
using UnityEditor;
using System;

namespace Core.UI.UIEditor
{
    [CustomEditor(typeof(FocusContainer))]
    public class FocusContainerEditor : Editor
    {
        private FocusContainer componentTarget;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            componentTarget = (FocusContainer)target;
            Selection.selectionChanged = OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            componentTarget.SelectPage(Selection.activeTransform);
        }
    }
}