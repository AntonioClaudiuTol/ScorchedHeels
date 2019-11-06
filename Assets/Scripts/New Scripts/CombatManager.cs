using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Character player;
    [SerializeField] private static List<Enemy> enemies;

    private void OnValidate()
    {
        AcquireTargets();
    }

    private void OnEnable()
    {
        Character.OnDealDamage += PassDamageInformationToEnemy;
        Enemy.OnDealDamage += PassDamageInformationToPlayer;
        Enemy.OnEnemyDeath += RemoveEnemy;
    }

    private void OnDisable()
    {
        Character.OnDealDamage -= PassDamageInformationToEnemy;
        Enemy.OnDealDamage -= PassDamageInformationToPlayer;
        Enemy.OnEnemyDeath -= RemoveEnemy;
    }

    private void PassDamageInformationToEnemy(int damage)
    {
        if (enemies.Count > 0)
        {
            Debug.Log("Current target is: " + enemies[0].name);
            enemies[0].TakeDamage(damage);
        }
    }
    
    private void PassDamageInformationToPlayer(int damage)
    {
        player.TakeDamage(damage);
    }

    private void RemoveEnemy()
    {
        if (enemies.Count > 0)
        {
            if (enemies.Count > 1)
            Debug.Log(enemies[0].name + " died. Current target is: " + enemies[1].name);
            Enemy oldEnemy = enemies[0];
            enemies.Remove(enemies[0]);
            Destroy(oldEnemy.gameObject);
        }
    }

    private void AcquireTargets()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = new List<Enemy>();
        foreach (GameObject gameObject in go)
        {
            enemies.Add(gameObject.GetComponent<Enemy>());
        }
        
        enemies.Sort((enemy, enemy1) => String.Compare(enemy.name, enemy1.name));
        firstenemy = enemies[0];
    }

    public static Enemy firstenemy;

    public static Enemy GetNextEnemy()
    {
        if (enemies.Count > 0)
        {
            enemies.Remove(enemies[0]);
        }
            
        if (enemies.Count > 0)
        {
            enemies[0].State = EnemyState.Attacking;
            return enemies[0];
        }

        return null;
    }

    public static Enemy GetEnemyNoDelete()
    {
        if (enemies.Count > 0)
        {
            enemies[0].State = EnemyState.Attacking;
            return enemies[0];
        }

        return null;
    }
}
