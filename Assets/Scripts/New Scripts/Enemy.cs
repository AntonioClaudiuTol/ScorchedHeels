﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = System.Random;

public enum EnemyState
{
    Idle,
    Attacking
}
public class Enemy : MonoBehaviour
{
    public CharacterStat Damage;
    public CharacterStat Defense;
    public CharacterStat HPRegen;
    
    [SerializeField] StatPanel statPanel;
    [SerializeField] private HealthBarEnemy healthBar;
    [SerializeField] private Text nameDisplay;
    [SerializeField] string name;
    [SerializeField] public float maximumHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] float damage;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private float attackCooldown = 0;
    [SerializeField] List<Item> items;
    private Character target;
    public EnemyState State = EnemyState.Idle;
    
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
        State = EnemyState.Idle;
        InitStats();
        healthBar = GameObject.Find("Health Bar Enemy").GetComponent<HealthBarEnemy>();
        healthBar.UpdateValues(currentHealth, maximumHealth);
        statPanel = GameObject.Find("Enemy Panel").GetComponentInChildren<StatPanel>();
        statPanel.SetStats(Damage, Defense, HPRegen);
        statPanel.UpdateStatValues();
        nameDisplay = GameObject.Find("Enemy Name").GetComponent<Text>();
        UpdateName();
    }

    private void UpdateName()
    {
        nameDisplay.text = name;
    }

    private void InitStats()
    {
        maximumHealth = Damage.Value * 10;
        currentHealth = maximumHealth;
        damage = Damage.Value;
    }

    private void Update()
    {
//        if(currentHealth < maximumHealth)
//        {
//            State = EnemyState.Attacking;
//        }

//        State = EnemyState.Idle;

        if (State == EnemyState.Idle)
        {
            StopAllCoroutines();
        }
        if (State == EnemyState.Attacking && !startedCoroutine)
        {
            UpdateName();
            healthBar.UpdateValues(currentHealth, maximumHealth);
            statPanel.SetStats(Damage, Defense, HPRegen);
            statPanel.UpdateStatValues();
            StartCoroutine(Combat());
            startedCoroutine = true;
        }
    }


    public delegate void DamageDealing(string damage);
    public static event DamageDealing OnDamageDealt;

    public delegate void DealDamage(float damage);

    public static event DealDamage OnDealDamage;
    
    public delegate void EnemyDeath();

    public static event EnemyDeath OnEnemyDeath;
        
    
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

//    public void DealDamage(int amount)
//    {
//        target.Health -= amount;
//    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateValues(currentHealth, maximumHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    
    
    /// <summary>
    ///
    ///
    /// enemy has stats
    /// enemy spawns
    /// enemy attacks
    /// enemy dies
    /// enemy drops items
    /// 
    /// </summary>
    

    private bool died = false;

    private void Die()
    {
        if(!died)
        {
            died = true;
            State = EnemyState.Idle;
            StopAllCoroutines();

            CombatLog.LogCombatEventStatic(gameObject.name + " has died.");
            
            if(UnityEngine.Random.Range(0, 100) > 25)
            {
                if (OnDealDamage != null)
                {
                    OnDealDamage(damage);
                }
                if(OnDamageDealt != null)
                {
                    OnDamageDealt("<color=yellow>" + this.gameObject.name + "</color> dropped a <color=green>" + items[0].name + "</color>.");
                }
                target.Inventory.AddItem(items[0]);    
            }
            if(UnityEngine.Random.Range(0, 100) > 25)
            {
                if(OnDamageDealt != null)
                {
                    OnDamageDealt("<color=yellow>" + this.gameObject.name + "</color> dropped a <color=green>" + items[1].name + "</color>.");
                }
                target.Inventory.AddItem(items[1]);    
            }

            
            if (OnEnemyDeath != null)
            {
                OnEnemyDeath();
//                Destroy(gameObject);
            }
            
        }
        
//        Destroy(gameObject);
    }
    
    IEnumerator Combat()
    {
        while (true)
        {
            target.TakeDamage(damage);
            if(OnDamageDealt != null)
            {
                OnDamageDealt("<color=yellow>" + this.gameObject.name + "</color> has dealt <color=red>" + damage.ToString() + "</color> damage to <color=blue>" + target.name + "</color>.");
            }
            yield return waitForSeconds;
        }
    }
	
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
}
