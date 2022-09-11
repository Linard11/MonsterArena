using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private Monster monsterA;
    [SerializeField] private Monster monsterB;

    [SerializeField] private MonsterUI monsterAUI;
    [SerializeField] private MonsterUI monsterBUI;

    [SerializeField] private TextMeshProUGUI commentaryText;

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
        RegisterNewMonster(monsterA, monsterAUI);
        RegisterNewMonster(monsterB, monsterBUI);
        
        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
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
    }


    private void RegisterNewMonster(Monster monster, MonsterUI monsterUI)
    {
        UpdateTitle(monster, monsterUI);
        UpdateHealth(monster, monsterUI);
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
