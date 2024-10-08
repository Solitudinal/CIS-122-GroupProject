using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

// Allows easy creation of Player objects from Project window
[CreateAssetMenu(fileName = "Player", menuName = "Player/Create new player")]
public class CombatPlayerBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite backSprite;

    // Stores information about which department-specific powers player should have
    [SerializeField] PlayerForm form;

    // Base Stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

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

    // Enum stores references to department-specific powers that player has earned
    public enum PlayerForm
    {
    None,
    Biology,
    Physics,
    Gym
    }
}
