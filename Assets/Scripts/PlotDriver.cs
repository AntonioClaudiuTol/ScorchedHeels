using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlotDriver : MonoBehaviour
{
    public Text storyText;
    public Text buttonText;
    public UnityEngine.UI.Button gatherButton;

    private string plot0 = "Are you ready to start?";
    private string plot1 = "You wake up with a skullsplitting headache. Your vision is blurry and you have no idea where you are. Wait a minute, you don't even know WHO you are.";
    private string plot2 = "You find yourself in a forest glade. It's morning and the sun bathes you in its warm light. Birds chirp nearby, insects buzz and your stomach growls. It's time to get your bearings!";
    private string plot3 = "Branches litter the forest floor and a few berry bushes are nearby. Time to eat!";
    private string buttonPlot0 = "Wake Up";
    private string buttonPlot1 = "Look Around";
    private string buttonPlot2 = "Too Tired";
    private string buttonPlot3 = "Explore";
    private string[] plotPoints;
    private string[] buttonTexts;
    private int step = 1;
    
    void Start()
    {
        plotPoints = new string[] { plot0, plot1, plot2, plot3 };
        buttonTexts = new string[] { buttonPlot0, buttonPlot1, buttonPlot2, buttonPlot3 };
        storyText.text = plotPoints[0];
        buttonText.text = buttonTexts[0];
    }

    public void UpdateButtonState()
    {
        if (step < plotPoints.Length)
        {
            storyText.text = plotPoints[step];
        }

        if (step < buttonTexts.Length)
        {
            buttonText.text = buttonTexts[step];
        }

        step++;

        //if (step == plotPoints.Length)
        //{
        //    storyText.enabled = false;
        //}
        if (step + 1 == buttonTexts.Length)
        {
            buttonText.transform.parent.GetComponent<UnityEngine.UI.Button>().interactable = false;
            gatherButton.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(Resources.Energy > 20)
        {
            buttonText.transform.parent.GetComponent<UnityEngine.UI.Button>().interactable = true;
            //buttonText.text = buttonTexts[step++];
        }
    }
}
