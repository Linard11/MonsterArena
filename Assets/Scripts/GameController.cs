using TMPro;

using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private MonsterUI monsterAUI;
    [SerializeField] private MonsterUI monsterBUI;

    [SerializeField] private Monster monsterA;
    [SerializeField] private Monster monsterB;

    [SerializeField] private TextMeshProUGUI commentaryText;

    private void Start()
    {
        RegisterNewMonster(monsterA, monsterAUI);
        RegisterNewMonster(monsterB, monsterBUI);
        
        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
    }

    private void RegisterNewMonster(Monster newMonster, MonsterUI newMonsterUI)
    {
        UpdateTitle(newMonster, newMonsterUI);
        UpdateHealth(newMonster, newMonsterUI);
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
