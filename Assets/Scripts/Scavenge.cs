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
    
    public delegate void LogAction(string message);
    public static event LogAction OnLogAction;
    
    
    
    private string message = "You fumble around in the darkness hoping to find something useful.";

    private void OnValidate()
    {
        Inventory = GameObject.FindWithTag("Player").GetComponent<Character>().Inventory;
    }

    public void StartScavenging()
    {
        LogEvent(message);
        scavengingButton.interactable = false;
        Invoke(nameof(ScavengeResults), 2);
    }

    public void ScavengeResults()
    {
//        if (Random.Range(0, 100) < 25f)
        {
            if (Inventory.AddItem(possibleDrops[0]))
            {
                LogEvent("While scavenging, you found a <color=green>" + possibleDrops[0].name + "</color>.");
            }    
        }
//        if (Random.Range(0, 100) < 25f)
        {
            if (Inventory.AddItem(possibleDrops[1]))
            {
                LogEvent("While scavenging, you found a <color=green>" + possibleDrops[1].name + "</color>.");
            }    
        }
        
        scavengingButton.interactable = true;
        if (Inventory.ContainsItem(possibleDrops[0]) && Inventory.ContainsItem(possibleDrops[1]))
        {
            craftingButton.gameObject.SetActive(true);
            LogEvent("Looks like you have enough materials for a makeshift torch.(Crafting unlocked.)");
        }
        
    }

    public void LogEvent(string logMessage)
    {
        if(OnLogAction != null)
        {
            OnLogAction(logMessage);
        }
    }
}
