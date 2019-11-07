using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public delegate void LogAction(string message);
    public static event LogAction OnLogAction;
    public Button scavengeButton;

    private string message = "You wake up with a skullsplitting headache. Your vision is blurry and you have no idea where you are. Wait a minute, you don't even know WHO you are.";
    
    public void StartGameAction()
    {
        LogEvent(message);
        scavengeButton.gameObject.SetActive(true);
        Destroy(gameObject);
        
    }

    public void LogEvent(string logMessage)
    {
        if(OnLogAction != null)
        {
            OnLogAction(logMessage);
        }
    }
    
    
}
