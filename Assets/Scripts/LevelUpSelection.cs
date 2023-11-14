using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelection : MonoBehaviour
{
    //create a list for all the attacks the player can get
    public List<IPlayerAttack> attacks;
    
    private void Start()
    {
        //create three rectangular UI panels for the player to select from, each containing a different attack
        //create a button for each attack
        //when the player clicks on a button, call the LevelUp() method on the corresponding attack
        //when the player clicks on a button, destroy the UI panels
        
    }

    void DisplayUIPanels()
    {
    }
}
