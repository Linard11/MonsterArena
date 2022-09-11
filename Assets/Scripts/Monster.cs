using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private string title = "Monster";
    [SerializeField] private float maxHealth = 10f;

    [Space]
    
    [SerializeField] private string attackName = "ATTACKENNAME";
    [SerializeField] private float attackStrength = 2f;
    [SerializeField] private GameObject attackEffect;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public string Attack(Monster target)
    {
        float damage = attackStrength;
        target.UpdateHealth(-damage);

        Instantiate(attackEffect, target.transform.position, Quaternion.identity);

        return GetAttackString(target, damage);
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

    private string GetAttackString(Monster target, float damage)
    {
        string text = $"{title} wendet {attackName} an.";

        text += $" {target.title} erleidet {damage:F1} Schaden.";

        if (target.HasFainted())
        {
            text += $" {target.title} ist ohnmächtig geworden.";
        }

        return text;
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
