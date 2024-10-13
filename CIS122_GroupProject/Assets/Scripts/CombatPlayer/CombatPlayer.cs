// Written by Philip Jacobson
// 10/5/2024

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for constructing a main character game object that is used exclusively in combat
public class CombatPlayer
{
    // Attributes for combat version of main character
    public CombatPlayerBase Base {  get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public List<Ability> Abilities { get; set; }

    // Constructor
    public CombatPlayer(CombatPlayerBase pBase, int pLevel)
    {
        this.Base = pBase;
        this.Level = pLevel;
        HP = this.MaxHp;

        // Checks if player is the appropriate level to have access to specific abilities
        Abilities = new List<Ability>();
        foreach (var ability in this.Base.LearnableAbilities)
        {
            if (ability.Level <= this.Level)
            {
                this.Abilities.Add(new Ability(ability.Base));
            }
        }
    }

    // Formulas that determine current stats using current level and base stats.
    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }

    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; } // Note that HP adds by 10 rather than 5
    }
}