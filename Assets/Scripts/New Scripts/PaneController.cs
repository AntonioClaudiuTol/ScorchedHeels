using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaneController : MonoBehaviour
{
    [SerializeField] private List<GameObject> panes;
    
    public delegate void PaneUnlock();

    public static event PaneUnlock OnPaneUnlock;

    private void OnEnable()
    {
        StartGame.OnActivatePane += ActivatePane;
        Scavenge.OnActivatePane += ActivatePane;
        ScavengingUpgrades.OnActivatePane += ActivatePane;
    }

    private void OnDisable()
    {
        StartGame.OnActivatePane -= ActivatePane;
        Scavenge.OnActivatePane -= ActivatePane;
        ScavengingUpgrades.OnActivatePane -= ActivatePane;
    }

    private void Awake()
    {
        foreach (var pane in panes)
        {
            pane.SetActive(true);
//            pane.SetActive(false);
        }
    }

    private void ActivatePane(int paneNumber)
    {
        /*
         * 0 - Location
         * 1 - Inventory
         * 2 - Crafting
         * 3 - Training
         * 4 - Dungeon
         */
        panes[paneNumber].SetActive(true);
    }
}
