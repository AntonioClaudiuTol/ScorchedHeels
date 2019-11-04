using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private static List<Enemy> enemies;

    private void OnValidate()
    {
        AcquireTargets();
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
    
//    player si current enemy hp text si bars
//        
//        list of enemies
//    vreau combat log in care sa vad damage si drops
}
