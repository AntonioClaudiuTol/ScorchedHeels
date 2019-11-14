using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField] private GameObject combatPanel;
    [SerializeField] private Character player;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Enemy> enemyPrefabs;
    public List<Enemy> oldEnemies;
    private Enemy enemyTemp;


    private void Awake()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.GetComponent<Enemy>());
        }

        oldEnemies = new List<Enemy>();
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    public void OpenDungeonMenu()
    {
        combatPanel.SetActive(!combatPanel.activeSelf);
    }

    public void ResetDungeon()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        enemies = null;
        enemies = new List<Enemy>();

        foreach (var enemyPrefab in enemyPrefabs)
        {
            enemies.Add(Instantiate(enemyPrefab).GetComponent<Enemy>());
        }

        CombatManager.enemies = enemies;
        player.Health = player.maxHealth;
    }
}