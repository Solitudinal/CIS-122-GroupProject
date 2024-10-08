using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
