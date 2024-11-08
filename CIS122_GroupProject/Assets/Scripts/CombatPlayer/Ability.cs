// Written by Philip Jacobson
// 10/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script contains data for specific abilities
public class Ability
{
    public AbilityBase Base { get; set; }
    public int SP { get; set; }

    // Constructor
    public Ability(AbilityBase pBase)
    {
        Base = pBase;
        SP = pBase.SP;
    }
}
