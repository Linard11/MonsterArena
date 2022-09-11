using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoBehaviour
{
    public const float NormalDamageMultiplier = 1.0f;
    public const float EffectiveDamageMultiplier = 1.5f;
    public const float IneffectiveDamageMultiplier = 0.5f;

    [SerializeField] private string title = "Monster";

    [Header("Attack")]
    [SerializeField] private string attackName = "ATTACKENNAME";
    [SerializeField] private float attackStrength = 2f;
    [SerializeField] private Element attackElement = Element.Normal;
    [SerializeField] private GameObject attackEffect;
    
    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private UnityEvent onDamaged;
    [SerializeField] private UnityEvent onFainted;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public string Attack(Monster target)
    {
        float attackMultiplier = GetDamageMultiplier(this.attackElement, target.attackElement);
        float damage = attackStrength * attackMultiplier;
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

        if (change < 0)
        {
            if (currentHealth > 0)
            {
                onDamaged.Invoke();
            }
            else
            {
                onFainted.Invoke();
            }
        }
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

    private float GetDamageMultiplier(Element attackerElement, Element defenderElement)
    {
        // TODO Calculate a damage multiplier based on the interacting elements
        return 1;
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
