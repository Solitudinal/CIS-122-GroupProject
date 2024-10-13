// Written by Philip Jacobson
// 10/5/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script takes information about the CombatPlayer object to display information about the player in the combat HUD
public class BattleHUD : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] HPBar hpBar;

    public void SetData(CombatPlayer player)
    {
        nameText.text = player.Base.Name;
        levelText.text = "Lvl " + player.Level;
        hpBar.SetHP((float) player.HP / player.MaxHp);
    }
}
