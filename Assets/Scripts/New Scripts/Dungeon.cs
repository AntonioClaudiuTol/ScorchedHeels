using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField] private GameObject combatPanel;
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
        //TODO: Stop combat and restore player to full health after a dungeon reset.
    }
}