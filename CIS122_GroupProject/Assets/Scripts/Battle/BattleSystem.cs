// Written by Philip Jacobson
// 10/12/2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerAbility, EnemyTurn, Busy}

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

    // This coroutine is called when player selects an ability
    IEnumerator PerformPlayerAbility()
    {
        // Switches state from PlayerAbility to Busy so that player can't change current ability while it is being executed
        state = BattleState.Busy;  

        var ability = playerUnit.Player.Abilities[currentAbility];

        yield return dialogueBox.TypeDialogue($"{playerUnit.Player.Base.Name} used {ability.Base.Name}!");
        yield return new WaitForSeconds(1f);

        bool isDefeated = enemyUnit.Player.TakeDamage(ability, playerUnit.Player);

        yield return enemyHud.UpdateHP(enemyUnit.Player);

        if (isDefeated)
        {
            yield return dialogueBox.TypeDialogue($"{enemyUnit.Player.Base.Name} has been vanquished!");
        }
        else
        {
            StartCoroutine(EnemyTurn()); // Passes turn over to the enemy if they still have HP
        }
    }

    // Coroutine that handles enemy actions
    IEnumerator EnemyTurn()
    {
        state = BattleState.EnemyTurn;

        var ability = enemyUnit.Player.GetRandomAbility();

        yield return dialogueBox.TypeDialogue($"{enemyUnit.Player.Base.Name} used {ability.Base.Name}!");
        yield return new WaitForSeconds(1f);

        bool isDefeated = playerUnit.Player.TakeDamage(ability, enemyUnit.Player);

        yield return playerHud.UpdateHP(playerUnit.Player);

        if (isDefeated)
        {
            yield return dialogueBox.TypeDialogue($"{playerUnit.Player.Base.Name} has been vanquished!");
        }
        else
        {
            PlayerAction(); // Passes turn back to the player if they still have HP
        }
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

        // Executes the selected ability when player presses 'z'
        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogueBox.EnableAbilitySelector(false);
            dialogueBox.EnableDialogueText(true);

            StartCoroutine(PerformPlayerAbility());
        }
    }
}
