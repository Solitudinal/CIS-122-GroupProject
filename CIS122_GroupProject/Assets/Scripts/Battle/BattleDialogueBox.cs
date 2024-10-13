// Written by Philip Jacobson
// 10/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script determines and updates all text in the dialogue box at the bottom of the combat scene
public class BattleDialogueBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] Color highlightedColor;

    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject abilitySelector;
    [SerializeField] GameObject abilityDetails;

    [SerializeField] List<TMP_Text> actionTexts;
    [SerializeField] List<TMP_Text> abilityTexts;

    [SerializeField] TMP_Text spText;
    [SerializeField] TMP_Text formText;

    public void SetDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
    }

    // Method that animates the combat dialogue
    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";

        foreach (var letter in dialogue)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond); // Waits for specified time between adding each letter
        }
    }

    // Methods that allow enabling/disabling battle dialogue elements
    public void EnableDialogueText(bool enabled)
    {
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }

    public void EnableAbilitySelector(bool enabled)
    {
        abilitySelector.SetActive(enabled);
        abilityDetails.SetActive(enabled);
    }

    // Method that loops through action text options and highlights the one that is passed as an argument
    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                actionTexts[i].color = highlightedColor;
            }
            else
            {
                actionTexts[i].color = Color.black;
            }
        }
    }

    // Method that loops through ability text options and highlights the one that is passed as an argument
    public void UpdateAbilitySelection(int selectedAbility, Ability ability)
    {
        for (int i = 0; i < abilityTexts.Count; i++)
        {
            if (i == selectedAbility)
            {
                abilityTexts[i].color = highlightedColor;
            }
            else
            {
                abilityTexts[i].color = Color.black;
            }
        }

        // Also updates SP and Form texts to show selected ability details
        spText.text = $"SP {ability.SP}/{ability.Base.SP}";
        formText.text = ability.Base.Form.ToString(); // Form is an enum, so we convert to a string
    }

    // Method that loops through abilities of player and sets their name appropriately in the dialogue box
    public void SetAbilityNames(List<Ability> abilities)
    {
        for (int i = 0; i < abilityTexts.Count; i++)
        {
            if (i < abilities.Count)
            {
                abilityTexts[i].text = abilities[i].Base.Name;
            }
            else
            {
                abilityTexts[i].text = "-"; // If player has less than 4 abilities, sets "-" instead.
            }
        }
    }
}
