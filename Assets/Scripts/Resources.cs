using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    public static int Energy { get; set; }
    public static int Branch { get; set; }
    public static int Rock { get; set; }
    public static int Berry { get; set; }

    public Text energyText;
    public Text energyValue;
    public Text branchText;
    public Text branchValue;
    public Text berryText;
    public Text berryValue;
    public Text rockText;
    public Text rockValue;

    public static bool isEnergyUnlocked = false;
    public static bool isBranchUnlocked = false;
    public static bool isBerryUnlocked = false;
    public static bool isRockUnlocked = false;

    private void Start()
    {
        Energy = 5;
        Branch = 0;
        Rock = 0;
        Berry = 0;
        energyText.enabled = false;
        energyValue.enabled = false;
        branchText.enabled = false;
        branchValue.enabled = false;
        berryText.enabled = false;
        berryValue.enabled = false;
        rockText.enabled = false;
        rockValue.enabled = false;
    }

    private void Update()
    {
        UnlockEnergy();
        UnlockBranch();
        UnlockBerry();
        UnlockRock();
    }

    private void UnlockRock()
    {
        if (isRockUnlocked)
        {
            rockText.enabled = true;
            rockValue.enabled = true;
            rockValue.text = Rock.ToString();
        }
    }

    private void UnlockBerry()
    {
        if (isBerryUnlocked)
        {
            berryText.enabled = true;
            berryValue.enabled = true;
            berryValue.text = Berry.ToString();
        }
    }

    private void UnlockBranch()
    {
        if (isBranchUnlocked)
        {
            branchText.enabled = true;
            branchValue.enabled = true;
            branchValue.text = Branch.ToString();
        }
    }

    private void UnlockEnergy()
    {
        if (isEnergyUnlocked)
        {
            energyText.enabled = true;
            energyValue.enabled = true;
            energyValue.text = Energy.ToString();
        }
    }
}
