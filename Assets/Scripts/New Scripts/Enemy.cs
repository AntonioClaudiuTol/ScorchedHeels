using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ICombatant
{
    [SerializeField] string name;
    [SerializeField] int maximumHealth;
    [SerializeField] int currentHealth;
    [SerializeField] int damage;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private float attackCooldown = 0;
    [SerializeField] List<Item> items;
    private Character target;
    [SerializeField] private Inventory inventory;
    public UnityEvent deathEvent;
    
    public Enemy()
    {
        name = "Enemy";
        maximumHealth = 50;
        currentHealth = maximumHealth;
        damage = 10;
        
    }

    private void Awake()
    {
        target  = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        attackCooldown += Time.deltaTime;

        if (attackCooldown >= attackSpeed)
        {
            DealDamage(damage);
            attackCooldown = 0;
        }
    }

    public void DealDamage(int amount)
    {
        target.Health -= amount;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        deathEvent.Invoke();

        target.Inventory.AddItem(items[0]);
        target.Inventory.AddItem(items[1]);
        Destroy(gameObject);
        
    }
}
