using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour {

    #region Variables

    int freesp = 10;
    int[] Skills=new int[3];
    [HideInInspector]
    public bool SkillsActive=false;

    #endregion

    #region Initialization
    private void Start()
    {
        //read from serialization
        for (int i = 0; i < Skills.Length; i++)
            Skills[i] = Random.Range(0, 3);
    }

    #endregion

    #region Methods

    private void Update()
    {
        //read from serialization
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!SkillsActive)
            {
                
            }
            else
            {

            }
            SkillsActive = !SkillsActive;
            transform.GetChild(0).gameObject.SetActive(SkillsActive);
            //GetComponentInChildren<Transform>().gameObject.SetActive(SkillsActive);
        }
    }

    //Level up a certain skill
    public void LevelUpSkill(int SkillID)
    {
        if (1 > freesp)
            MessageManager.getInstance().DissplayMessage("Insufficient Funds(Like in the bank)", 3);
        else
        {
            Skills[SkillID]++;
            MessageManager.getInstance().DissplayMessage("Skill succesfully upgraded", 3);
        }
        
    }
    //Give the player a certain amount of skill points
    public void GainSkillPoint(int value)
    {
        freesp += value;
    }
    //Return the number of free skill points
    public int AvailableSkillPoints()
    {
        return freesp;
    }

    #endregion

}
