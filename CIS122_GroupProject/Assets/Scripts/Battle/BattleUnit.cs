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

        PlayEnterAnimation();
    }

    // Method for animation of player/enemy sprites at the start of combat
    public void PlayEnterAnimation()
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

    // Method for animation of player/enemy sprites when executing attacks
    public void PlayAttackAnimation()
    {
        // Sequence() enables playing multiple animations sequentially
        var sequence = DOTween.Sequence();

        // A movement of 50 x-units to the right or left is appended to the first part of sequence
        if (isPlayerUnit)
        {
            sequence.Append(image.transform.DOLocalMoveX(originalPosition.x + 50f, 0.25f));
        }
        else
        {
            sequence.Append(image.transform.DOLocalMoveX(originalPosition.x - 50f, 0.25f));
        }

        // A return to the original position is appended to the second and final part of animation sequence
        sequence.Append(image.transform.DOLocalMoveX(originalPosition.x, 0.25f));
    }
}
