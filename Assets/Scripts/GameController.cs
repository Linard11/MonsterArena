using TMPro;

using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Monster monsterA;
    [SerializeField] private Monster monsterB;

    [SerializeField] private MonsterUI monsterAUI;
    [SerializeField] private MonsterUI monsterBUI;

    [SerializeField] private TextMeshProUGUI commentaryText;
    
    private void Start()
    {
        RegisterNewMonster(monsterA, monsterAUI);
        RegisterNewMonster(monsterB, monsterBUI);
        
        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
    }
    
    private void RegisterNewMonster(Monster monster, MonsterUI monsterUI)
    {
        UpdateTitle(monster, monsterUI);
        UpdateHealth(monster, monsterUI);
    }

    private void UpdateTitle(Monster monster, MonsterUI monsterUI)
    {
        
    }

    private void UpdateHealth(Monster monster, MonsterUI monsterUI)
    {
        
    }
}
