using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class Enemy : MonoBehaviour, ICombatant
{
    [SerializeField] string name;
    [SerializeField] int maximumHealth;
    [SerializeField] public int currentHealth;
    [SerializeField] int damage;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private float attackCooldown = 0;
    [SerializeField] List<Item> items;
    private Character target;
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
        if(currentHealth < maximumHealth)
        {
            startedCombat = true;
        }

        if (startedCombat && !startedCoroutine)
        {
            StartCoroutine(Combat());
            startedCoroutine = true;
        }
    }

    private bool startedCombat = false;
    private bool startedCoroutine = false;

    public void Attack()
    {
//        if (currentHealth < maximumHealth)
//        {
//            attackCooldown += Time.deltaTime;
//
//            if (attackCooldown >= attackSpeed)
//            {
//                DealDamage(damage);
//                attackCooldown = 0;
//            }
//        }
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

        if(UnityEngine.Random.Range(0, 100) > 25)
        {
            target.Inventory.AddItem(items[0]);    
        }
        if(UnityEngine.Random.Range(0, 100) > 25)
        {
            target.Inventory.AddItem(items[1]);    
        }
        
        Destroy(gameObject);
    }
    
    IEnumerator Combat()
    {
        while (true)
        {
            target.TakeDamage(damage);
            yield return waitForSeconds;
        }
    }
	
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    public void StartCombat()
    {
        StartCoroutine(Combat());
    }
}
