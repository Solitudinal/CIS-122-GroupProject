// Written by Philip Jacobson
// 10/5/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script enables visual changes in the HP bar seen during combat
public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    /// <summary>
    /// Updates image of HP bar whenever player's health changes
    /// </summary>
    /// <param name="hpNormalized"></param>
    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector2(hpNormalized, 1f);
    }
}
