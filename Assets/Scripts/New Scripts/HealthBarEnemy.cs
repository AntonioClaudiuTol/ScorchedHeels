using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthAmount;

    private void OnValidate()
    {
        InitHealthBar();
    }

    private void Awake()
    {
        InitHealthBar();
    }

    private void InitHealthBar()
    {
        healthBar = GetComponentsInChildren<Image>()[1];
        healthAmount = GetComponentInChildren<Text>();
    }

    public void UpdateValues(float currentHealth, float maximumHealth)
    {
        healthAmount.text = currentHealth + "/" + maximumHealth;
        if (currentHealth >= 0)
        {
            healthBar.transform.localScale = new Vector3(currentHealth / maximumHealth, 1f, 1f);
        }
        else
        {
            healthBar.transform.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}
