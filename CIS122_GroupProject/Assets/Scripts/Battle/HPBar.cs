// Written by Philip Jacobson
// 10/5/2024

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// This script enables visual changes in the HP bar seen during combat
public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    // Updates image of HP bar whenever player's health changes
    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector2(hpNormalized, 1f);
    }

    // Coroutine that smoothly updates current HP
    public IEnumerator SetHPSmoothly(float newHp)
    {
        float currentHp = health.transform.localScale.x;
        float changeAmount = currentHp - newHp;

        // Loop repeatedly reduces HP by a tiny amount until the difference between currentHp and newHp is very close to zero.
        while (currentHp - newHp > Mathf.Epsilon)
        {
            currentHp -= changeAmount * Time.deltaTime;

            health.transform.localScale = new Vector2(currentHp, 1f);

            yield return null; // After reducing HP by small amount, stops coroutine and continues it in the next frame.
        }

        // Need to explicitly set the transform here because the while loop exits just before reaching the actual HP value
        health.transform.localScale = new Vector2(newHp, 1f);
    }
}
