using System;
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
        if (State == EnemyState.Attacking)
        {
            UpdateName();
            healthBar.UpdateValues(currentHealth, maximumHealth);
            statPanel.SetStats(Damage, Defense, HPRegen);
            statPanel.UpdateStatValues();
            Attack();
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

    private bool died = false;
    
    private float itemDropChanceReduction = 0f;

    private void Die()
    {
        if(!died)
        {
            died = true;
            State = EnemyState.Idle;
            StopAllCoroutines();

            CombatLog.LogCombatEventStatic(gameObject.name + " has died.");

            foreach (var item in items)
            {
                if(UnityEngine.Random.Range(0, 100) < 25 / (1 + itemDropChanceReduction))
                {
                    itemDropChanceReduction++;
                    if (OnDealDamage != null)
                    {
                        OnDealDamage(damage);
                    }
                    if(OnDamageDealt != null)
                    {
                        OnDamageDealt("<color=yellow>" + gameObject.name + "</color> dropped a <color=green>" + item.name + "</color>.");
                    }
                    target.Inventory.AddItem(item);    
                }
            }
            
//            if(UnityEngine.Random.Range(0, 100) > 25)
//            {
//                if (OnDealDamage != null)
//                {
//                    OnDealDamage(damage);
//                }
//                if(OnDamageDealt != null)
//                {
//                    OnDamageDealt("<color=yellow>" + this.gameObject.name + "</color> dropped a <color=green>" + items[0].name + "</color>.");
//                }
//                target.Inventory.AddItem(items[0]);    
//            }
//            if(UnityEngine.Random.Range(0, 100) > 25)
//            {
//                if(OnDamageDealt != null)
//                {
//                    OnDamageDealt("<color=yellow>" + this.gameObject.name + "</color> dropped a <color=green>" + items[1].name + "</color>.");
//                }
//                target.Inventory.AddItem(items[1]);    
//            }

            
            if (OnEnemyDeath != null)
            {
                OnEnemyDeath();
            }
        }
    }
    
//    IEnumerator Combat()
//    {
//        while (true)
//        {
//            target.TakeDamage(damage);
//            if(OnDamageDealt != null)
//            {
//                OnDamageDealt("<color=yellow>" + this.gameObject.name + "</color> has dealt <color=red>" + damage.ToString() + "</color> damage to <color=blue>" + target.name + "</color>.");
//            }
//            yield return waitForSeconds;
//        }
//    }
    
    private void Attack()
    {
        attackCooldown += Time.deltaTime;

        if (attackCooldown >= attackSpeed)
        {
            if (OnDamageDealt != null)
            {
                OnDamageDealt("<color=blue>" + this.name + "</color> has dealt <color=red>" + damage.ToString() +
                              "</color> damage to <color=yellow> some enemy" + "</color>.");
            }

            if (OnDealDamage != null)
            {
                OnDealDamage(damage);
            }

            attackCooldown = 0f;
        }
    }

}
