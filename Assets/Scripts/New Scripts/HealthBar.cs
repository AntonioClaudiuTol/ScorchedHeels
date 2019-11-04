using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthAmount;

    private void OnValidate()
    {
        healthAmount.text = character.Health + "/" + character.maxHealth;
        healthBar.transform.localScale = new Vector3((float) character.Health / character.maxHealth, 1f, 1f);
    }

    private void Update()
    {
        healthAmount.text = character.Health + "/" + character.maxHealth;
        if (character.Health >= 0)
        {
            healthBar.transform.localScale = new Vector3((float) character.Health / character.maxHealth, 1f, 1f);
        }
    }
}
