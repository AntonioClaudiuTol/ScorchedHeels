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
    [SerializeField] private Image scavengingImage;

    public delegate void LogAction(string message);

    public static event LogAction OnLogAction;

    private float initialScavengingDuration = 2;
    private float scavengingDuration = 2;
    private float scavengingProgress = 0;
    private int scavengingUpgradeLevel = 0;
    private float scavengingDurationReduction = 0;
    private bool startedScavenge = false;
    private bool scaveningInProgress = false;
    private bool craftingUnlocked = false;
    

    private string message = "You fumble around in the darkness hoping to find something useful.";

    private void OnEnable()
    {
        ScavengingUpgrades.OnUpgrade += UpgradeScavengingLevel;
    }

    private void OnDisable()
    {
        ScavengingUpgrades.OnUpgrade -= UpgradeScavengingLevel;
    }

    private void UpgradeScavengingLevel()
    {
        scavengingUpgradeLevel++;
        scavengingDurationReduction = 0.2f * scavengingUpgradeLevel;
        scavengingDuration = initialScavengingDuration - scavengingDurationReduction;
        Debug.Log("Scavenging Duration: " + scavengingDuration);
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
                scavengingProgress = 0;
                StopScavenging();
            }
        }
    }

    public void StartScavengingProcess()
    {
        startedScavenge = !startedScavenge;
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
        if (Random.Range(0, 100) < 90f)
        {
            foundItem = true;
            if (Inventory.AddItem(possibleDrops[2]))
            {
                LogEvent("While scavenging, you found a <color=green>" + possibleDrops[2].name + "</color>.");
            }
        }

        if (!foundItem)
        {
            LogEvent("You found nothing.");
        }

        scavengingButton.interactable = true;
        if (Inventory.ContainsItem(possibleDrops[0]) && Inventory.ContainsItem(possibleDrops[1]) && !craftingUnlocked)
        {
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