using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Scavenge : MonoBehaviour
{
    [SerializeField] Inventory Inventory;
    [SerializeField] private Item[] possibleDrops;
    [SerializeField] private Button craftingButton;
    [SerializeField] private Button scavengingButton;
    [SerializeField] private Button stopScavengingButton;
    [SerializeField] private Image scavengingImage;

    public delegate void LogAction(string message);
    public static event LogAction OnLogAction;
    public delegate void ActivatePane(int paneNumber);
    public static event ActivatePane OnActivatePane;
    public delegate void UpdateFood();
    public static event UpdateFood OnUpdateFood;

    private float initialScavengingDuration = 2;
    private float scavengingDuration = 2;
    private float scavengingProgress = 0;
    private int scavengingUpgradeLevel = 0;
    private float scavengingDurationReduction = 0;
    private bool startedScavenge = false;
    private bool scaveningInProgress = false;
    private bool craftingUnlocked = false;

    private int baseDropChance = 100;
    private int scavengingSkillLevel = 0;
    

    private string message = "You fumble around in the darkness hoping to find something useful.";

    private void OnEnable()
    {
        ScavengingUpgrades.OnUpgradeScavengingSpeed += UpgradeScavengingSpeedLevel;
        ScavengingUpgrades.OnUpgradeScavengingSkill += UpgradeScavengingSkillLevel;
    }

    private void OnDisable()
    {
        ScavengingUpgrades.OnUpgradeScavengingSpeed -= UpgradeScavengingSpeedLevel;
        ScavengingUpgrades.OnUpgradeScavengingSkill -= UpgradeScavengingSkillLevel;
    }

    private void UpgradeScavengingSpeedLevel()
    {
        scavengingUpgradeLevel++;
        scavengingDurationReduction = 0.2f * scavengingUpgradeLevel;
        scavengingDuration = initialScavengingDuration - scavengingDurationReduction;
    }
    
    private void UpgradeScavengingSkillLevel()
    {
        scavengingSkillLevel++;
    }

    private void OnValidate()
    {
        Inventory = GameObject.FindWithTag("Player").GetComponent<Character>().Inventory;
        scavengingImage.transform.localScale = new Vector3(0, 1, 1);
    }

    private void Update()
    {
        if (startedScavenge)
        {
            if (!scaveningInProgress)
            {
                scaveningInProgress = !scaveningInProgress;
                StartScavenging();
            }
            scavengingProgress += Time.deltaTime;
//            scavengingImage.transform.localScale = new Vector3(scavengingProgress * (0.5f + 0.06f * scavengingUpgradeLevel), 1, 1);
            scavengingImage.transform.localScale = new Vector3(scavengingProgress/scavengingDuration, 1, 1);
            if (scavengingProgress >= scavengingDuration)
            {
                if (cancelScavenge)
                {
                    stopScavengingButton.interactable = true;
                    stopScavengingButton.gameObject.SetActive(false);
                    startedScavenge = false;
                    cancelScavenge = false;
                }
                scavengingProgress = 0;
                StopScavenging();
            }
        }
    }

    private bool cancelScavenge = false;

    public void StopScavengeButtonAction()
    {
        cancelScavenge = true;
        stopScavengingButton.interactable = false;
    }

    public void StartScavengingProcess()
    {
        startedScavenge = !startedScavenge;
        stopScavengingButton.gameObject.SetActive(true);
    }

    public void StopScavenging()
    {
        scaveningInProgress = false;
//        startedScavenge = false;
        scavengingButton.interactable = false;
        scavengingImage.transform.localScale = new Vector3(0, 1, 1);
        ScavengeResults();
    }
    public void StartScavenging()
    {
        startedScavenge = true;
        LogEvent(message);
        scavengingButton.interactable = false;
//        Invoke(nameof(ScavengeResults), 2);
    }

    public void ScavengeResults()
    {
        bool foundItem = false;
        if (Random.Range(0, 100) < 25f)
        {
            foundItem = true;
            if (Inventory.AddItem(possibleDrops[0]))
            {
                LogEvent("While scavenging, you found a <color=green>" + possibleDrops[0].name + "</color>.");
            }
        }
        if (Random.Range(0, 100) < 25f)
        {
            foundItem = true;
            if (Inventory.AddItem(possibleDrops[1]))
            {
                LogEvent("While scavenging, you found a <color=green>" + possibleDrops[1].name + "</color>.");
            }
        }
        if (Random.Range(0, 100) < baseDropChance + (8f * scavengingSkillLevel))
        {
            foundItem = true;
            if (Inventory.AddItem(possibleDrops[2]))
            {
                LogEvent("While scavenging, you found a <color=green>" + possibleDrops[2].name + "</color>.");
            }
        }

        if (foundItem)
        {
            if (OnActivatePane != null)
            {
                OnActivatePane(1);
            }

            if (OnUpdateFood != null)
            {
                OnUpdateFood();
            }
        }
        else
        {
            LogEvent("You found nothing.");
        }

        scavengingButton.interactable = true;
        if (Inventory.ContainsItem(possibleDrops[0]) && Inventory.ContainsItem(possibleDrops[1]) && !craftingUnlocked)
        {
            if (OnActivatePane != null)
            {
                OnActivatePane(2);
            }
            craftingUnlocked = true;
            craftingButton.gameObject.SetActive(true);
            LogEvent("Looks like you have enough materials for a makeshift torch.(Crafting unlocked.)");
        }
        
        
    }

    public void LogEvent(string logMessage)
    {
        if (OnLogAction != null)
        {
            OnLogAction(logMessage);
        }
    }
}