// Written by Philip Jacobson
// 10/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerAbility, EnemyAbility, Busy}

// This script sets up the battle scene using information from both the player and enemy unit objects
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] BattleDialogueBox dialogueBox;

    BattleState state;
    int currentAction;
    int currentAbility;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();

        playerHud.SetData(playerUnit.Player);
        enemyHud.SetData(enemyUnit.Player);

        dialogueBox.SetAbilityNames(playerUnit.Player.Abilities);

        yield return dialogueBox.TypeDialogue($"A wild {enemyUnit.Player.Base.Name} appeared!");
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    // Methods that change BattleState and update battle text appropriately
    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));
        dialogueBox.EnableActionSelector(true);
    }

    void PlayerAbility()
    {
        state = BattleState.PlayerAbility;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableAbilitySelector(true);
    }

    // Update the scene based on BattleState changes
    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerAbility)
        {
            HandleAbilitySelection();
        }
    }

    // Method that determines player action based on user input
    void HandleActionSelection()
    {
        // If user presses down arrow when top option is highlighted, game highlights bottom action
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
            {
                currentAction++;
            }
        }
        // If user presses up arrow when bottom option is highlighted, game highlights top action
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                currentAction--;
            }
        }

        // Invokes method that highlights action selected by the user
        dialogueBox.UpdateActionSelection(currentAction);

        // When user presses Z, switches BattleState based on action selection
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                // Fight
                PlayerAbility();
            }
            else if (currentAction == 1)
            {
                // Run
            }
        }
    }

    // Method that determines player ability used based on user input
    void HandleAbilitySelection()
    {
        // Right and left arrows cycle through abilities
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAbility < playerUnit.Player.Abilities.Count - 1)
            {
                currentAbility++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAbility > 0)
            {
                currentAbility--;
            }
        }
        // Up and down arrows cycle through abilities as well
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAbility < playerUnit.Player.Abilities.Count - 2)
            {
                currentAbility += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAbility > 1)
            {
                currentAbility -= 2;
            }
        }

        dialogueBox.UpdateAbilitySelection(currentAbility, playerUnit.Player.Abilities[currentAbility]);
    }
}
