// Written by Philip Jacobson
// 10/5/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script takes information about the CombatPlayer object to display information about player/enemies in the combat HUD
public class BattleHUD : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] HPBar hpBar;

    // Method that assigns appropriate values to both player and enemies in BattleSystem
    public void SetData(CombatPlayer player)
    {
        nameText.text = player.Base.Name;
        levelText.text = "Lvl " + player.Level;

        hpBar.SetHP((float)player.HP / player.MaxHp);
    }

    // Method is called in BattleSystem whenever a player or enemy takes damage
    // HP bar visually altered to accurately reflect their current percentage of max HP
    public IEnumerator UpdateHP(CombatPlayer player)
    {
        yield return hpBar.SetHPSmoothly((float)player.HP / player.MaxHp);
    }
}
