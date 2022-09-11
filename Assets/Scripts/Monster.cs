using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private string title = "Monster";
    [SerializeField] private float maxHealth = 10f;
    
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}
