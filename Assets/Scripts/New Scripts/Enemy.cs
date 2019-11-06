using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public enum EnemyState
{
    Idle,
    Attacking
}
public class Enemy : MonoBehaviour
{
    [SerializeField] string name;
    [SerializeField] public int maximumHealth;
    [SerializeField] public int currentHealth;
    [SerializeField] int damage;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private float attackCooldown = 0;
    [SerializeField] List<Item> items;
    private Character target;
    public UnityEvent deathEvent;
    public EnemyState State;
    
    public Enemy()
    {
        name = "Enemy";
        maximumHealth = 50;
        currentHealth = maximumHealth;
        damage = 10;
    }

    private void OnEnable()
    {
//        Character.OnDamageDealt += TakeDamage()
    }

    private void OnDisable()
    {
//        Character.OnDamageDealt -= 
    }
    
    

    private void Awake()
    {
        target  = GameObject.FindWithTag("Player").GetComponent<Character>();
        State = EnemyState.Idle;

    }

    private void Update()
    {
        if(currentHealth < maximumHealth)
        {
            State = EnemyState.Attacking;
        }

        if (State == EnemyState.Attacking && !startedCoroutine)
        {
            StartCoroutine(Combat());
            startedCoroutine = true;
        }
    }

    private void OnValidate()
    {
//        ThisMethodCallsThatMethod(CombatLog.ThisReturnsAString, "asda");
    }

    public delegate void DamageDealing(string damage);
    public static event DamageDealing OnDamageDealt;

    public delegate void DealDamage(int damage);

    public static event DealDamage OnDealDamage;
    
    public delegate void EnemyDeath();

    public static event EnemyDeath OnEnemyDeath;
        
    
    
    public static void ThisMethodCallsThatMethod(Action<string> action, string input)
    {
       action(input);
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

//    public void DealDamage(int amount)
//    {
//        target.Health -= amount;
//    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        
    }

    private bool died = false;

    private void Die()
    {
        if(!died)
        {
            died = true;
            State = EnemyState.Idle;
            StopAllCoroutines();
            deathEvent.Invoke();

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
	
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    public void StartCombat()
    {
        StartCoroutine(Combat());
    }
}
