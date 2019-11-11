using UnityEngine;

public class StartGame : MonoBehaviour
{
    public delegate void LogAction(string message);
    public static event LogAction OnLogAction;
    public delegate void ActivatePane(int paneNumber);
    public static event ActivatePane OnActivatePane;

    [SerializeField] private GameObject parentPanel;
    public GameObject scavengeButton;

    private string message = "You wake up with a skullsplitting headache. Your vision is blurry and you have no idea where you are. Wait a minute, you don't even know WHO you are.";
    
    public void StartGameAction()
    {
        
        LogEvent(message);
        ActivateLocationPane(0);
        scavengeButton.SetActive(true);
        parentPanel.SetActive(false);
        Destroy(gameObject);
    }

    private void ActivateLocationPane(int i)
    {
        if (OnActivatePane != null)
        {
            OnActivatePane(0);
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
