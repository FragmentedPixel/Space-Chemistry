using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * Responsible for holding all the substance reactions and returning them based on search.
 */

[CreateAssetMenu(fileName = "Reaction Table", menuName = "Substance/New Reaction Table.")]
public class ReactionsTable : ScriptableObject
{
    public List<ReactionEq> table = new List<ReactionEq>();

    public List<ReactionEq> GetReactionsFor(sSubstance substToFind)
    {
        // Create new empty list.
        List<ReactionEq> searchedList = new List<ReactionEq>();

        foreach(ReactionEq reaction in table)
        {
            // if the substance is found on the first place, add it.
            if(reaction.first == substToFind)
            {
                searchedList.Add(reaction);
            }

            // if the substance is found on the second place and the reaction is reversible, add the reaction reverse.
            else if(reaction.second == substToFind && reaction.reversible)
            {
                ReactionEq newReaction = new ReactionEq(reaction.second, reaction.first, reaction.result);
                searchedList.Add(newReaction);
            }
        }

        return searchedList;
    }
}

[Serializable]
public class ReactionEq
{
    public ReactionEq(sSubstance _first, sSubstance _second, sSubstance _result)
    {
        first = _first;
        second = _second;
        result = _result;

        reversible = false;
    }

    public sSubstance first;
    public sSubstance second;
    public sSubstance result;

    public bool reversible;
}
