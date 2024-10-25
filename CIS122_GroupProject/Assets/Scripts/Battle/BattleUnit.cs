// Written by Philip Jacobson
// 10/12/2024

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

// TODO : Explanation of script purpose 
public class BattleUnit : MonoBehaviour
{
    [SerializeField] CombatPlayerBase theBase;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public CombatPlayer Player { get; set; }

    Image image;
    Vector2 originalPosition;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPosition = image.transform.localPosition;
    }

    public void Setup()
    {
        Player = new CombatPlayer(theBase, level);

        // Assigns the appropriate sprite in the combat scene for either players or enemies
        if (isPlayerUnit)
        {
            image.sprite = Player.Base.BackSprite;
        }
        else
        {
            image.sprite = Player.Base.FrontSprite;
        }

        PlayerEnterAnimation();
    }

    // Method for animation of player/enemy sprites at the start of combat
    public void PlayerEnterAnimation()
    {
        // Sets player and enemy 500 x-units away from their original position
        if (isPlayerUnit)
        {
            image.transform.localPosition = new Vector2(-500f, originalPosition.y);
        }
        else
        {
            image.transform.localPosition = new Vector2(500f, originalPosition.y);
        }

        // Moves player/enemy back to original position. 
        // 1f sets length of animation.
        image.transform.DOLocalMove(originalPosition, 1f); 
    }
}
