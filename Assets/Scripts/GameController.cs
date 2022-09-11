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
        
        
        commentaryText.SetText($"{monsterA.GetTitle()} trifft auf {monsterB.GetTitle()}!");
    }
}
