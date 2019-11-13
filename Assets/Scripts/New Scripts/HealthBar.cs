using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthAmount;

    private void OnEnable()
    {
        Character.OnHealthUpdate += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        Character.OnHealthUpdate -= UpdateHealthDisplay;
    }

    private void OnValidate()
    {
        healthAmount.text = character.Health + "/" + character.maxHealth;
        healthBar.transform.localScale = new Vector3((float) character.Health / character.maxHealth, 1f, 1f);
    }

    private void Awake()
    {
        healthBar = GetComponentsInChildren<Image>()[1];
        healthAmount = GetComponentInChildren<Text>();
    }
    
    public void UpdateHealthDisplay()
    {
        healthAmount.text = character.Health + "/" + character.maxHealth;
        if (character.Health >= 0)
        {
            healthBar.transform.localScale = new Vector3((float) character.Health / character.maxHealth, 1f, 1f);
        }
    }
}
