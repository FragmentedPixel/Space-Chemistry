
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ReactionsTable))]
public class ReactionTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ReactionsTable myTarget = (ReactionsTable)target;

       

        foreach (ReactionEq reaction in myTarget.table)
        {
            EditorGUILayout.BeginHorizontal();
            reaction.first = (sSubstance) EditorGUILayout.ObjectField(reaction.first, typeof(sSubstance), false);
            GUILayout.Label("+");
            reaction.second = (sSubstance)EditorGUILayout.ObjectField(reaction.second, typeof(sSubstance), false);
            GUILayout.Label("=");
            reaction.result = (sSubstance)EditorGUILayout.ObjectField(reaction.result, typeof(sSubstance), false);
            GUILayout.Space(15f);
            reaction.reversible = EditorGUILayout.Toggle(reaction.reversible);
            

            EditorGUILayout.EndHorizontal();
        }

        GUI.color = Color.green;
        if (GUILayout.Button("Add reaction", GUILayout.Width(100f)))
        {
            myTarget.table.Add(new ReactionEq());
        }
        
        EditorUtility.SetDirty(myTarget);
    }
}