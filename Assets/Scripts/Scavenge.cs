using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scavenge : MonoBehaviour
{
    [SerializeField] Inventory Inventory;
    [SerializeField] private Item[] possibleDrops;
    [SerializeField] private Button craftingButton;
    
    public delegate void LogAction(string message);
    public static event LogAction OnLogAction;
    
    
    
    private string message = "You fumble around in the darkness hoping to find something useful.";
   
    public void StartScavenging()
    {
        LogEvent(message);
        Invoke(nameof(ScavengeResults), 2);
    }

    public void ScavengeResults()
    {
        if (Inventory.AddItem(possibleDrops[0]))
        {
            LogEvent("While scavenging, you found a <color=green>" + possibleDrops[0].name + "</color>.");
        }
        if (Inventory.AddItem(possibleDrops[1]))
        {
            LogEvent("While scavenging, you found a <color=green>" + possibleDrops[1].name + "</color>.");
        }
        
        craftingButton.gameObject.SetActive(true);
    }

    public void LogEvent(string logMessage)
    {
        if(OnLogAction != null)
        {
            OnLogAction(logMessage);
        }
    }
}
