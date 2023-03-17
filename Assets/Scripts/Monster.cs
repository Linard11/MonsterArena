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

    public string Attack(Monster target)
    {
        float damage = attackStrength;
        // TODO Multiply attack strength by 2 or 0.5 based on elements.
        
        // TODO Spawn particle effect prefab
        
        // TODO Play TakeDamage or Fainting animation
        // Tip. try use events
        // onDamged
        // onFainted

        target.UpdateHealth(-damage);

        return GetAttackString(target, damage);
    }

    private string GetAttackString(Monster target, float damage)
    {
        string text = $"{this.title} wendet {this.attackName} an.";

        text += $" {target.title} erleidet {damage:F1} Schaden.";

        if (target.HasFainted())
        {
            text += $" {target.title} ist ohnmächtig geworden.";
        }

        return text;
    }

    public bool HasFainted()
    {
        return currentHealth <= 0;
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
