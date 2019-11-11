using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScavengingUpgrades : MonoBehaviour
{
    [SerializeField] private Text scavengingSpeedUpgradeText;
    [SerializeField] private Button upgradeScavengingSpeedButton;
    [SerializeField] private Text scavengingSkillUpgradeText;
    [SerializeField] private Button upgradeScavengingSkillButton;
    [SerializeField] private Text gathererText;
    [SerializeField] private Button gathererUpgradeButton;
    [SerializeField] private Text dryingText;
    [SerializeField] private Button dryingUpgradeButton;
    [SerializeField] private Text combatTrainingText;
    [SerializeField] private Button combatTrainingUpgradeButton;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Item food;
    private int scavengeSpeedLevel = 0;
    private int scavengeSpeedBaseCost = 5;
    private int scavengeSkillLevel = 0;
    private int scavengeSkillBaseCost = 10;
    private int gathererBaseCost = 12;
    private int dryingBaseCost = 14;
    private int combatTrainingBaseCost = 4;

    public delegate void UpgradeScavengingSpeedEvent();
    public static event UpgradeScavengingSpeedEvent OnUpgradeScavengingSpeed;
    public delegate void UpgradeScavengingSkillEvent();
    public static event UpgradeScavengingSkillEvent OnUpgradeScavengingSkill;
    public delegate void ActivatePane(int paneNumber);
    public static event ActivatePane OnActivatePane;

    private void Awake()
    {
        scavengingSpeedUpgradeText.text = "Scavenging Speed (" + scavengeSpeedBaseCost + " food)";
        scavengingSkillUpgradeText.text = "Scavenging Skill (" + scavengeSkillBaseCost + " food)";
        gathererText.text = "Gatherer (" + gathererBaseCost + " food)";
        dryingText.text = "Drying (" + dryingBaseCost + " food)";
        combatTrainingText.text = "Combat Training (" + combatTrainingBaseCost + " food)";
        upgradeScavengingSpeedButton.interactable = false;
        upgradeScavengingSkillButton.interactable = false;
        gathererUpgradeButton.interactable = false;
        dryingUpgradeButton.interactable = false;
        combatTrainingUpgradeButton.interactable = false;
        upgradeScavengingSpeedButton.gameObject.SetActive(false);
        upgradeScavengingSkillButton.gameObject.SetActive(false);
        gathererUpgradeButton.gameObject.SetActive(false);
        dryingUpgradeButton.gameObject.SetActive(false);
        combatTrainingUpgradeButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Scavenge.OnUpdateFood += CheckFoodReserve;
    }

    private void OnDisable()
    {
        Scavenge.OnUpdateFood -= CheckFoodReserve;
    }

    public void CheckFoodReserve()
    {
        if (inventory.ContainsItem(food))
        {
            int foodCount = inventory.ItemCount(food.ID);
            if (foodCount >= scavengeSpeedBaseCost / 2)
            {
                upgradeScavengingSpeedButton.gameObject.SetActive(true);
            }
            if (foodCount >= scavengeSkillBaseCost / 2)
            {
                upgradeScavengingSkillButton.gameObject.SetActive(true);
            }
            if (foodCount >= gathererBaseCost / 2)
            {
                gathererUpgradeButton.gameObject.SetActive(true);
            }
            if (foodCount >= dryingBaseCost / 2)
            {
                dryingUpgradeButton.gameObject.SetActive(true);
            }
            if (foodCount >= combatTrainingBaseCost / 2)
            {
                if (combatTrainingUpgradeButton != null)
                {
                    combatTrainingUpgradeButton.gameObject.SetActive(true);                    
                }
            }
            
            if(foodCount >= scavengeSpeedBaseCost * (1 + scavengeSpeedLevel))
            {
                upgradeScavengingSpeedButton.interactable = true;
            }
            else
            {
                upgradeScavengingSpeedButton.interactable = false;
            }
            if(foodCount >= scavengeSkillBaseCost * (1 + scavengeSkillLevel))
            {
                upgradeScavengingSkillButton.interactable = true;
            }
            else
            {
                upgradeScavengingSkillButton.interactable = false;
            }
            if(foodCount >= gathererBaseCost)
            {
                gathererUpgradeButton.interactable = true;
            }
            else
            {
                gathererUpgradeButton.interactable = false;
            }
            if(foodCount >= dryingBaseCost)
            {
                dryingUpgradeButton.interactable = true;
            }
            else
            {
                dryingUpgradeButton.interactable = false;
            }
            if(foodCount >= combatTrainingBaseCost)
            {
                combatTrainingUpgradeButton.interactable = true;
            }
            else
            {
                combatTrainingUpgradeButton.interactable = false;
            }
        }
    }

    public void ChangeScavengingSpeedUpgradeText(int upgradeLevel)
    {
        scavengingSpeedUpgradeText.text = "Scavenging Speed Increase" + "(" + scavengeSpeedBaseCost * (1 + upgradeLevel) + " food)";
    }
    
    public void ChangeScavengingSkillUpgradeText(int upgradeLevel)
    {
        scavengingSkillUpgradeText.text = "Scavenging Skill Increase" + "(" + scavengeSpeedBaseCost * (1 + upgradeLevel) + " food)";
    }

    public void UpgradeScavengingSpeed()
    {
        if (inventory.RemoveItem(food.ID, scavengeSpeedBaseCost * (1 + scavengeSpeedLevel)))
        {
            CheckFoodReserve();
            upgradeScavengingSpeedButton.interactable = false;
            ChangeScavengingSpeedUpgradeText(++scavengeSpeedLevel);
            if (OnUpgradeScavengingSpeed != null)
            {
                OnUpgradeScavengingSpeed();
            }

            if (scavengeSpeedLevel >= 5)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void UpgradeScavengingSkill()
    {
        if (inventory.RemoveItem(food.ID, scavengeSkillBaseCost * (1 + scavengeSkillLevel)))
        {
            CheckFoodReserve();
            upgradeScavengingSkillButton.interactable = false;
            ChangeScavengingSkillUpgradeText(++scavengeSkillLevel);
            if (OnUpgradeScavengingSkill != null)
            {
                OnUpgradeScavengingSkill();
            }

            if (scavengeSkillLevel >= 5)
            {
                Destroy(gameObject);
            }
        }
    }
    
    public void UpgradeGathering()
    {
        CheckFoodReserve();
        //TODO: increase drop chance for wood and cloth by 5% per level
    }

    public void UnlockDrying()
    {
        CheckFoodReserve();
        //TODO: double food stack size
    }

    public void UnlockCombatTraining()
    {
        
        if (OnActivatePane != null)
        {
            if (inventory.RemoveItem(food.ID, combatTrainingBaseCost))
            {
                OnActivatePane(3);
                CheckFoodReserve();
                Destroy(combatTrainingUpgradeButton.gameObject);
            }
        }
    }
}