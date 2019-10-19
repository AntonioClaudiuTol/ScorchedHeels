using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gathering : MonoBehaviour
{
    public Button gatherButton;
    public Button eatButton;
    public Text energy;
    private int location = 0;
    public Boolean isEatUnlocked = false;


    private void Update()
    {
        if (gatherButton.enabled)
        {
            Resources.isEnergyUnlocked = true;
        }

        if (Resources.Energy == 0)
        {
            gatherButton.interactable = false;
        }
        else
        {
            gatherButton.interactable = true;
        }

        if (isEatUnlocked)
        {
            eatButton.gameObject.SetActive(true);
        }
        if(Resources.Berry == 0)
        {
            eatButton.interactable = false;
        }
        else
        {
            eatButton.interactable = true;
        }
    }

    public void Gather()
    {
        if(Resources.Energy > 0)
        {
            UpdateEnergyValue();
            switch (location)
            {
                case 0:
                    {
                        GatherInForestGlade();
                        break;
                    }
            }
        }
    }

    private void UpdateEnergyValue()
    {
        Resources.Energy--;
        energy.text = Resources.Energy.ToString();
    }

    private void GatherInForestGlade()
    {
        if (UnityEngine.Random.Range(0, 100) < 50)
        {
            if(!Resources.isBranchUnlocked)
            {
                Resources.isBranchUnlocked = true;
            }
            Resources.Branch++;
        }
        else
        {
            if (!Resources.isBerryUnlocked)
            {
                Resources.isBerryUnlocked = true;
            }

            if(!isEatUnlocked)
            {
                isEatUnlocked = true;
            }
            Resources.Berry++;
        }
    }
}
