using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Monster> monsterPrefabs;

    [SerializeField] private Transform monsterSlotA;
    [SerializeField] private Transform monsterSlotB;

    [SerializeField] private MonsterUI monsterAUI;
    [SerializeField] private MonsterUI monsterBUI;

    [SerializeField] private TextMeshProUGUI commentaryText;

    private Monster monsterA;
    private Monster monsterB;
    
    private GameInput input;

    private bool isMonsterATurn = true;
    
    private void Awake()
    {
        input = new GameInput();
        input.Player.Next.performed += PerformNextAction;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void Start()
    {
        StartNewBattle();
    }
    
    private void OnDisable()
    {
        input.Disable();
    }

    private void OnDestroy()
    {
        input.Player.Next.performed -= PerformNextAction;
    }
    
    
    private void PerformNextAction(InputAction.CallbackContext context)
    {
        // TODO Implement spawning new monsters
        if (monsterA.HasFainted() || monsterB.HasFainted())
        {
            return;
        }
        
        Monster attacker;
        Monster defender; // attackee
        MonsterUI defenderUI;

        if (isMonsterATurn) // Monster A greift an
        {
            attacker = monsterA;
            defender = monsterB;
            defenderUI = monsterBUI;
        }
        else // Monster B greift an
        {
            attacker = monsterB;
            defender = monsterA;
            defenderUI = monsterAUI;
        }

        string attackDescription = attacker.Attack(defender);
        UpdateHealth(defender, defenderUI);
        
        commentaryText.SetText(attackDescription);

        isMonsterATurn = !isMonsterATurn;
    }

    private void StartNewBattle()
    {
        // Select a random number between 0 and the element count of the monster prefab list (exclusive).
        int challengerAIndex = Random.Range(0, monsterPrefabs.Count);
        int challengerBIndex = Random.Range(0, monsterPrefabs.Count);
        
        // Try again if picking the same two monsters.
        if (monsterPrefabs.Count >= 2)
        {
            while (challengerAIndex == challengerBIndex)
            {
                challengerBIndex = Random.Range(0, monsterPrefabs.Count);
            }
        }

        // Use the selected number to pick the corresponding monster.
        Monster challengerA = monsterPrefabs[challengerAIndex];
        Monster challengerB = monsterPrefabs[challengerBIndex];
        
        // Spawn new monsters and update their UIs.
        monsterA = RegisterNewMonster(challengerA, monsterSlotA, monsterAUI);
        monsterB = RegisterNewMonster(challengerB, monsterSlotB, monsterBUI);
        
        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
    }

    private Monster RegisterNewMonster(Monster monsterPrefab, Transform monsterSlot, MonsterUI monsterUI)
    {
        // TODO Remove old monsters in slots
        
        // Spawn monster from prefab.
        Monster newSpawned = Instantiate(monsterPrefab, monsterSlot);
        
        // Link the UI to the monster.
        UpdateTitle(newSpawned, monsterUI);
        UpdateHealth(newSpawned, monsterUI);
        
        return newSpawned;
    }

    private void UpdateTitle(Monster monster, MonsterUI monsterUI)
    {
        monsterUI.UpdateTitle(monster.GetTitle());
    }

    private void UpdateHealth(Monster monster, MonsterUI monsterUI)
    {
        monsterUI.UpdateHealth(monster.GetCurrentHealth(), monster.GetMaxHealth());
    }
}
