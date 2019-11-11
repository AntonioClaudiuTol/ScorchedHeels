using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScavengingUpgrades : MonoBehaviour
{
    [SerializeField] private Text scavengingUpgradeText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Item food;
    private int level = 0;
    public delegate void UpgradeScavenging();

    public static event UpgradeScavenging OnUpgrade;

    private void Awake()
    {
        upgradeButton.interactable = false;
        scavengingUpgradeText.text = "Scavenging Speed Increase(5 food)";
    }

    private void Update()
    {
        CheckFoodReserve();
    }

    public void CheckFoodReserve()
    {
        if(inventory.ContainsItem(food) && inventory.ItemCount(food.ID) >= 5 * (1 + level))
        {
            upgradeButton.interactable = true;
        }
    }
    

    public void ChangeScavengingUpgradeText(int upgradeLevel)
    {
        scavengingUpgradeText.text = "Scavenging Speed Increase" + "(" + 5 * (1 + upgradeLevel) + " food)";
    }
    
    public void UpgradeScavengingSpeed()
    {
        inventory.RemoveItem(food);
        upgradeButton.interactable = false;
        ChangeScavengingUpgradeText(++level);
        if (OnUpgrade != null)
        {
            OnUpgrade();
        }
        if(level >= 5)
        {
            Destroy(gameObject);
        }
    }
}
