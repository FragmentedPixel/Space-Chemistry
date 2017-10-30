using UnityEngine;
using System.Collections;
using UnityEditor;

/*
 * Responsible for creating a visual interface for the Reaction Table.
 */

[CustomEditor(typeof(ReactionsTable))]
public class ReactionTableEditor : Editor
{
    private Color initColor;

    public override void OnInspectorGUI()
    {
        // References.
        ReactionsTable myTarget = (ReactionsTable)target;
        initColor = GUI.color;

        // Add all rows.
        for(int i = 0; i < myTarget.table.Count; i++)
        {
            ReactionEq reaction = myTarget.table[i];

            // Display current row.
            EditorGUILayout.BeginHorizontal();
            reaction.first = (sSubstance) EditorGUILayout.ObjectField(reaction.first, typeof(sSubstance), false);
            GUILayout.Label("+");
            reaction.second = (sSubstance)EditorGUILayout.ObjectField(reaction.second, typeof(sSubstance), false);
            GUILayout.Label("=");
            reaction.result = (sSubstance)EditorGUILayout.ObjectField(reaction.result, typeof(sSubstance), false);
            GUILayout.Space(15f);
            reaction.reversible = EditorGUILayout.Toggle(reaction.reversible);

            //Remove button.
            GUI.color = Color.red;
            if (GUILayout.Button("X", GUILayout.Width(20f)))
            {
                myTarget.table.Remove(reaction);
                i--;
            }
            GUI.color = initColor;

            EditorGUILayout.EndHorizontal();
        }


        // Add items to the list.
        GUI.color = Color.green;
        if (GUILayout.Button("Add reaction", GUILayout.Width(100f)))
        {
            myTarget.table.Add(new ReactionEq(null, null, null));
        }

        //Update script.
        EditorUtility.SetDirty(myTarget);
    }
}