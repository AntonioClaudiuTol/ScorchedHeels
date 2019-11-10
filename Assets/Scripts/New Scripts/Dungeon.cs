using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField] private GameObject combatPanel;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Enemy> enemyPrefabs;


    private void Awake()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }

    public void OpenDungeonMenu()
    {
        combatPanel.SetActive(!combatPanel.activeSelf);
    }

    public void ResetDungeon()
    {
        enemies.Clear();
        foreach (var enemyPrefab in enemyPrefabs)
        {
            Instantiate(enemyPrefab);
        }
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }
}
