using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Enemy character;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthAmount;

    private void OnValidate()
    {
//       healthAmount.text = character.Health + "/" + character.maxHealth;
//        healthBar.transform.localScale = new Vector3((float) character.Health / character.maxHealth, 1f, 1f);
    }



    private void Update()
    {
        character = CombatManager.GetEnemyNoDelete();
        if(character != null)
        {
            UpdateValues(character.currentHealth, character.maximumHealth);
        }
//        healthBar.transform.localScale = new Vector3(0.2f, 1f, 1f);
    }

    public void UpdateValues(int currentHealth, int maximumHealth)
    {
        healthAmount.text = currentHealth + "/" + maximumHealth;
        if (currentHealth >= 0)
        {
            healthBar.transform.localScale = new Vector3((float) currentHealth / maximumHealth, 1f, 1f);
        }
        else
        {
            healthBar.transform.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}
