// Written by Philip Jacobson
// 10/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script assigns the appropriate sprite in the combat scene for either players or enemies
public class BattleUnit : MonoBehaviour
{
    [SerializeField] CombatPlayerBase theBase;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public CombatPlayer Player { get; set; }

    public void Setup()
    {
        Player = new CombatPlayer(theBase, level);

        
        if (isPlayerUnit)
        {
            GetComponent<Image>().sprite = Player.Base.BackSprite;
        }
        else
        {
            GetComponent<Image>().sprite = Player.Base.FrontSprite;
        }
        
    }
}
