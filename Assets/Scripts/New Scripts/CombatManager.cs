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
        Character.OnDeath += StopCombat;
    }


    private void OnDisable()
    {
        Character.OnDealDamage -= PassDamageInformationToEnemy;
        Enemy.OnDealDamage -= PassDamageInformationToPlayer;
        Enemy.OnEnemyDeath -= RemoveEnemy;
        Character.OnDeath += StopCombat;
    }

    private void PassDamageInformationToEnemy(float damage)
    {
        if (enemies.Count > 0)
        {
            enemies[0].TakeDamage(damage);
        }
    }

    private void PassDamageInformationToPlayer(float damage)
    {
        player.TakeDamage(damage);
    }

    private void RemoveEnemy()
    {
        if (enemies.Count > 0)
        {
            Enemy oldEnemy = enemies[0];
            enemies.Remove(enemies[0]);
            Destroy(oldEnemy.gameObject);
            StartCombat();
        }
    }

    private void StopCombat()
    {
        enemies[0].State = EnemyState.Idle;
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
    }

    public static Enemy GetEnemyNoDelete()
    {
        if (enemies.Count > 0)
        {
            return enemies[0];
        }

        return null;
    }

    public static void StartCombat()
    {
        if (enemies.Count > 0)
        {
            enemies[0].State = EnemyState.Attacking;
        }
    }
}