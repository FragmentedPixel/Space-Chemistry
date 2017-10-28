using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reaction Table", menuName = "Substance/New Reaction Table.")]
public class ReactionsTable : ScriptableObject
{
    public List<ReactionEq> table = new List<ReactionEq>();
}

[System.Serializable]
public class ReactionEq
{
    public sSubstance first;
    public sSubstance second;
    public sSubstance result;

    public bool reversible;
}
