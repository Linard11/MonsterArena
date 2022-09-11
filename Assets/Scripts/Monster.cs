using System.ComponentModel;

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

        return GetAttackString(target, damage, attackMultiplier);
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

    private string GetAttackString(Monster target, float damage, float multiplier)
    {
        string text = $"{title} wendet {attackName} an.";

        text += $" {target.title} erleidet {damage:F1} Schaden.";

        // TODO Add additional description based on the damage multiplier
        // " Es war sehr effektive!" for a multiplier above 1.01f
        // " Es war nicht sehr effektiv!" for a multiplier below 0.99f
        
        if (target.HasFainted())
        {
            text += $" {target.title} ist ohnmächtig geworden.";
        }

        return text;
    }

    private float GetDamageMultiplier(Element attackerElement, Element defenderElement)
    {
        switch (attackerElement)
        {
            case Element.Normal:
                switch (defenderElement)
                {
                    case Element.Normal:
                    case Element.Fire:
                    case Element.Water:
                    case Element.Plant:
                        return NormalDamageMultiplier;
                    case Element.Stone:
                        return IneffectiveDamageMultiplier;
                    default:
                        throw new InvalidEnumArgumentException(nameof(defenderElement), (int)defenderElement, typeof(Element)); 
                }
            // TODO Add the cases for the other elements
            default:
                return 1;
                //throw new InvalidEnumArgumentException(nameof(attackerElement), (int)attackerElement, typeof(Element));
        }
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
