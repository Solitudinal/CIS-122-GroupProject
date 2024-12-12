// Written by Philip Jacobson
// 10/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CombatPlayerBase;

// Allows easy creation of Ability objects from Project window
[CreateAssetMenu(fileName = "Ability", menuName = "Player/Create new ability")]

// This script handles information and stats about the various special moves the player has access to
// TODO: Implement SP-cost mechanic for abilities
public class AbilityBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] PlayerForm form;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int sp;

    // Gets
    public string Name
    {
        get { return this.name; }
    }

    public string Description
    {
        get { return this.description; }
    }

    public PlayerForm Form
    {
        get { return this.form; }
    }

    public int Power
    {
        get { return this.power; }
    }

    public int Accuracy
    {
        get { return this.accuracy; }
    }

    public int SP
    {
        get { return this.sp; }
    }
}
