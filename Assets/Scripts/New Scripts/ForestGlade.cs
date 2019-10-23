using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestGlade : MonoBehaviour
{
    public Button gladeButton;
    public Text text;
    public GameObject combatPanel;
    public bool isExplored = false;
    public void Explore()
    {
        if (isExplored)
        {
            ToggleCombat();
        }
        text.text = "Forest Glade - 1 AP";
        isExplored = true;

        
    }

    private void ToggleCombat()
    {
        combatPanel.SetActive(!combatPanel.activeSelf);
    }
}
