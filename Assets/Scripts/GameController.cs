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
        if (monsterA.HasFainted() || monsterB.HasFainted())
        {
            StartNewBattle();
            return;
        }
        
        Monster attacker;
        Monster defender;
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
        int challengerAIndex = Random.Range(0, monsterPrefabs.Count);
        int challengerBIndex = Random.Range(0, monsterPrefabs.Count);
        
        // TODO Generate new B index in case it's the same as A index
        // Tip: use a while loop

        Monster challengerA = monsterPrefabs[challengerAIndex];
        Monster challengerB = monsterPrefabs[challengerBIndex];

        monsterA = RegisterNewMonster(challengerA, monsterSlotA, monsterAUI);
        monsterB = RegisterNewMonster(challengerB, monsterSlotB, monsterBUI);
        
        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
    }

    private Monster RegisterNewMonster(Monster monsterPrefab, Transform monsterSlot, MonsterUI newMonsterUI)
    {
        ClearSlot(monsterSlot);

        Monster newSpawned = Instantiate(monsterPrefab, monsterSlot);
        
        UpdateTitle(newSpawned, newMonsterUI);
        UpdateHealth(newSpawned, newMonsterUI);

        return newSpawned;
    }

    private void ClearSlot(Transform slot)
    {
        for (int i = slot.childCount - 1; i >= 0; i--)
        {
            Transform child = slot.GetChild(i);
            Destroy(child.gameObject);
        }
    }
    
    private void UpdateTitle(Monster newMonster, MonsterUI newMonsterUI)
    {
        newMonsterUI.UpdateTitle(newMonster.GetTitle());
    }
    
    private void UpdateHealth(Monster newMonster, MonsterUI newMonsterUI)
    {
        newMonsterUI.UpdateHealth(newMonster.GetCurrentHealth(), newMonster.GetMaxHealth());
    }
}
