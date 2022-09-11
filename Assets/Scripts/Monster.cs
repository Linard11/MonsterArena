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

        if (multiplier > 1.01f)
        {
            text += " Es war sehr effektiv!";
        }
        else if (multiplier < 0.99f)
        {
            text += " Es war nicht sehr effektiv!";
        }
        
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
            case Element.Fire:
                switch (defenderElement)
                {
                    case Element.Plant:
                        return EffectiveDamageMultiplier;
                    case Element.Normal:
                    case Element.Fire:
                        return NormalDamageMultiplier;
                    case Element.Water:
                    case Element.Stone:
                        return IneffectiveDamageMultiplier;
                    default:
                        throw new InvalidEnumArgumentException(nameof(defenderElement), (int)defenderElement, typeof(Element)); 
                }
            case Element.Water:
                switch (defenderElement)
                {
                    case Element.Fire:
                    case Element.Stone:
                        return EffectiveDamageMultiplier;
                    case Element.Normal:
                    case Element.Water:
                        return NormalDamageMultiplier;
                    case Element.Plant:
                        return IneffectiveDamageMultiplier;
                    default:
                        throw new InvalidEnumArgumentException(nameof(defenderElement), (int)defenderElement, typeof(Element)); 
                }
            case Element.Stone:
                switch (defenderElement)
                {
                    case Element.Normal:
                    case Element.Fire:
                        return EffectiveDamageMultiplier;
                    case Element.Stone:
                    case Element.Plant:
                        return NormalDamageMultiplier;
                    case Element.Water:
                        return IneffectiveDamageMultiplier;
                    default:
                        throw new InvalidEnumArgumentException(nameof(defenderElement), (int)defenderElement, typeof(Element)); 
                }
            case Element.Plant:
                switch (defenderElement)
                {
                    case Element.Water:
                        return EffectiveDamageMultiplier;
                    case Element.Normal:
                    case Element.Stone:
                    case Element.Plant:
                        return NormalDamageMultiplier;
                    case Element.Fire:
                        return IneffectiveDamageMultiplier;
                    default:
                        throw new InvalidEnumArgumentException(nameof(defenderElement), (int)defenderElement, typeof(Element)); 
                }
            default:
                throw new InvalidEnumArgumentException(nameof(attackerElement), (int)attackerElement, typeof(Element));
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
