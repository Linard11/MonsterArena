using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private string title = "Monster";
    [SerializeField] private float maxHealth = 10f;

    [SerializeField] private string attackName = "ATTACKENNAME";
    [SerializeField] private float attackStrength = 2f;
    
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Attack(Monster target)
    {
        float damage = attackStrength;
        // TODO Add multiplier 0.5x or 2.0x depending on elements of attacker and defender.
        // Tip: Use a enum
        
        // TODO Spawn attack particle system above target
        
        // TODO Create a attack description and set it in the commentaryText

        target.UpdateHealth(-damage);
    }

    public bool HasFainted()
    {
        return currentHealth == 0;
    }

    private void UpdateHealth(float change)
    {
        currentHealth += change;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        // TODO Play TakeDamage or Fainting animation
        // Tip: Use UnityEvents
        // onDamaged
        // onFainting
        
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
