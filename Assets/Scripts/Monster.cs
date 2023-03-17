using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private string title = "Monster";
    [SerializeField] private float maxHealth = 10f;

    [SerializeField] private string attackName = "ATTACKENNAME";
    [SerializeField] private float attackStrength = 2f;
    
    // TODO Use Unity Events for Animations "TakeDamage" und "Fainting" im Animator Controller
    // onDamaged
    // onFainted
    // Invoke in UpdateHealth()

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Attack(Monster target)
    {
        float damage = attackStrength;
        // TODO use multiplier 0.5x and 2.0x for damage depending how which element attacks which element
        target.UpdateHealth(-damage);
        
        // TODO Spawn particle effect Prefab over target
        // Tip: Make a inspector field for the prefab
        
        // TODO Create string text to show who attacked who with how much damage
        // and display on commentaryText
    }

    public bool HasFainted()
    {
        return currentHealth == 0;
    }

    private void UpdateHealth(float change)
    {
        currentHealth += change;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public string GetTitle()
    {
        return title;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
