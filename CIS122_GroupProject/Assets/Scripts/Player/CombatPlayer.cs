using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayer
{
    CombatPlayerBase theBase;
    int level;

    public CombatPlayer(CombatPlayerBase pBase, int pLevel)
    {
        this.theBase = pBase;
        this.level = pLevel;
    }

    // Formulas that determine current stats using current level and base stats.
    public int Attack
    {
        get { return Mathf.FloorToInt((theBase.Attack * level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((theBase.Defense * level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((theBase.SpAttack * level) / 100f) + 5; }
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt((theBase.SpDefense * level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((theBase.Speed * level) / 100f) + 5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((theBase.MaxHp * level) / 100f) + 10; } // Note that HP adds by 10 rather than 5
    }
}
