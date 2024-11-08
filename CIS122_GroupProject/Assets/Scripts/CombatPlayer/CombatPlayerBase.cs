// Written by Philip Jacobson
// 10/5/2024

using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

// Allows easy creation of player/enemy combat objects from Project window
[CreateAssetMenu(fileName = "Player", menuName = "Player/Create new player")]

// Inheriting from ScriptableObject allows this script to be used independently from game objects
public class CombatPlayerBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite backSprite;
    [SerializeField] Sprite frontSprite;

    // Stores information about which department-specific powers player/enemies should have
    [SerializeField] PlayerForm form;

    // Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableAbility> learnableAbilities;

    // Gets
    public string Name
    {
        get { return this.name; }
    }

    public string Description
    {
        get { return this.description; }
    }

    public Sprite BackSprite
    {
        get { return this.backSprite; }
    }

    public Sprite FrontSprite
    {
        get { return this.frontSprite; }
    }

    public PlayerForm Form
    {
        get { return this.form; }
    }

    public int MaxHp
    {
        get { return this.maxHp; }
    }

    public int Attack
    {
        get { return this.attack; }
    }

    public int Defense
    {
        get { return this.defense; }
    }

    public int SpAttack
    {
        get { return this.spAttack; }
    }

    public int SpDefense
    {
        get { return this.spDefense; }
    }

    public int Speed
    {
        get { return this.speed; }
    }

    public List<LearnableAbility> LearnableAbilities
    {
        get { return this.learnableAbilities; }
    }

    // Class for abilities that player/enemies gain at specific levels
    [System.Serializable]
    public class LearnableAbility
    {
        [SerializeField] AbilityBase abilityBase;
        [SerializeField] int level;

        public AbilityBase Base
        {
            get { return this.abilityBase; }
        }

        public int Level
        {
            get { return this.level; }
        }
    }

    // Enum stores references to department-specific powers that player/enemies have access to
    public enum PlayerForm
    {
    None,
    Biology,
    Physics,
    Gym
    }
}
