using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : Editor
{
    [MenuItem("GameObject/UI/TabButton", false)]
    static void CreateTabButtonInScene(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = new GameObject("TabButton");
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        go.AddComponent<TabButton>();
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}

[CustomEditor(typeof(TabGroup))]
public class TabGroupEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as TabGroup;

        myScript.transition = (TabTransitionTypes)EditorGUILayout.EnumPopup("Transition", myScript.transition);

        switch (myScript.transition)
        {
            case TabTransitionTypes.ColorTint:
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Normal Color");
                    myScript.normalColor = EditorGUILayout.ColorField(myScript.normalColor);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Highlighted Color");
                    myScript.highlightedColor = EditorGUILayout.ColorField(myScript.highlightedColor);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Selected Color");
                    myScript.selectedColor = EditorGUILayout.ColorField(myScript.selectedColor);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Disabled Color");
                    myScript.disabledColor = EditorGUILayout.ColorField(myScript.disabledColor);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;
                    break;
                }
            case TabTransitionTypes.SpriteSwap:
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Normal Sprite");
                    myScript.normalSprite = (Sprite)EditorGUILayout.ObjectField(myScript.normalSprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Highlighted Sprite");
                    myScript.highlightedSprite = (Sprite)EditorGUILayout.ObjectField(myScript.highlightedSprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Selected Sprite");
                    myScript.selectedSprite = (Sprite)EditorGUILayout.ObjectField(myScript.selectedSprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Disabled Sprite");
                    myScript.disabledSprite = (Sprite)EditorGUILayout.ObjectField(myScript.disabledSprite, typeof(Sprite), allowSceneObjects: true);
                    EditorGUILayout.EndHorizontal();
                    EditorGUI.indentLevel--;
                    break;
                }
        }
    }
}
