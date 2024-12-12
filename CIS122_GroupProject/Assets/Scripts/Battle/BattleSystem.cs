// Written by Philip Jacobson
// 10/12/2024

// Some additions by Niloy Sarker Bappy (scene transition at end of combat)


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerAbility, EnemyTurn, Busy}

// This script manages the turn-based combat logic
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] BattleDialogueBox dialogueBox;

    [SerializeField] AudioSource cycleSound; // Sound for cycling through options
    [SerializeField] AudioSource selectSound; // Sound for pressing 'Z'

    BattleState state;

    int currentAction;
    int currentAbility;

    private void Start()
    {
        StartCoroutine(SetupBattle());
    }

    // Coroutine sets up the battle scene by fetching data about both player and enemy
    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();

        playerHud.SetData(playerUnit.Player);
        enemyHud.SetData(enemyUnit.Player);

        dialogueBox.SetAbilityNames(playerUnit.Player.Abilities);

        // Handles opening dialogue in combat 
        // TODO: Create a boss tag to trigger boss dialogue rather than checking for specific names
        if (enemyUnit.Player.Base.Name == "Bio Prof")
        {
            yield return dialogueBox.TypeDialogue($"{enemyUnit.Player.Base.Name} appears to be possessed by a malevolent force!");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return dialogueBox.TypeDialogue($"A wild {enemyUnit.Player.Base.Name} appeared!");
            yield return new WaitForSeconds(1f);
        }


        PlayerAction();
    }

    void EndBattle()
    {
        // Ends the battle and returns to the previous scene
        PlayerMovement.ReturnToPreviousScene();
    }


    // Methods that change BattleState and update battle text appropriately
    void PlayerAction()
    {
        state = BattleState.PlayerAction;

        StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));

        dialogueBox.EnableActionSelector(true);
    }

    // Method that updates HUD to show available abilities the player can select
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

        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        enemyUnit.PlayHitAnimation();

        bool isDefeated = enemyUnit.Player.TakeDamage(ability, playerUnit.Player);
        yield return enemyHud.UpdateHP(enemyUnit.Player);

        if (isDefeated)
        {
            yield return dialogueBox.TypeDialogue($"{enemyUnit.Player.Base.Name} has been vanquished!");
            enemyUnit.PlayDefeatAnimation();
            EndBattle(); // Ends the battle if the enemy is defeated
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

        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);

        playerUnit.PlayHitAnimation();

        bool isDefeated = playerUnit.Player.TakeDamage(ability, enemyUnit.Player);
        yield return playerHud.UpdateHP(playerUnit.Player);

        if (isDefeated)
        {
            yield return dialogueBox.TypeDialogue($"{playerUnit.Player.Base.Name} has been vanquished!");
            playerUnit.PlayDefeatAnimation();
            EndBattle(); // Ends the battle if the player is defeated
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
                cycleSound.Play(); 
            }
        }
        // If user presses up arrow when bottom option is highlighted, game highlights top action
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
            {
                currentAction--;
                cycleSound.Play();
            }
        }

        // Invokes method that highlights action selected by the user
        dialogueBox.UpdateActionSelection(currentAction);

        // When user presses Z, switches BattleState based on action selection
        if (Input.GetKeyDown(KeyCode.Z))
        {
            selectSound.Play(); 
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
                cycleSound.Play(); 
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAbility > 0)
            {
                currentAbility--;
                cycleSound.Play(); 
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAbility < playerUnit.Player.Abilities.Count - 2)
            {
                currentAbility += 2;
                cycleSound.Play(); 
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAbility > 1)
            {
                currentAbility -= 2;
                cycleSound.Play(); 
            }
        }

        dialogueBox.UpdateAbilitySelection(currentAbility, playerUnit.Player.Abilities[currentAbility]);

        // Executes the selected ability when player presses 'z'
        if (Input.GetKeyDown(KeyCode.Z))
        {
            selectSound.Play(); 
            dialogueBox.EnableAbilitySelector(false);
            dialogueBox.EnableDialogueText(true);

            StartCoroutine(PerformPlayerAbility());
        }
    }
}
