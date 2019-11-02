using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private List<Enemy> enemies;
    public GameObject enemy;

    private void Start()
    {
        Instantiate(enemy);
        
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject gameObject in go)
        {
            gameObject.GetComponent<Enemy>().deathEvent.AddListener(SpawnAnotherEnemy);
            enemies.Add(gameObject.GetComponent<Enemy>());
        }
        
    }

    public void SpawnAnotherEnemy()
    {
        Instantiate(enemy);
    }
}
