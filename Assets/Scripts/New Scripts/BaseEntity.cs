using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    /*
     * variables:
     * name
     * stats(hp, dmg, attack speed)
     * combatState(idle, attacking, dead)
     * 
     * methods:
     * attack
     * takeDamage
     *
     * events:
     * startCombat(idle -> attacking)
     * stopCombat(attacking -> idle)
     * death(idle/attacking -> dead)
     * 
     * 
     */

    private int currentHealth;
    private int maximumHealth;
    private int damage;
    private float attackSpeed;

    protected virtual int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }

    protected virtual int MaximumHealth
    {
        get => maximumHealth;
        set => maximumHealth = value;
    }

    protected virtual int Damage
    {
        get => damage;
        set => damage = value;
    }

    protected virtual float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }
}
