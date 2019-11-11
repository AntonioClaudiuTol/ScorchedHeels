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
    }

    private void OnDisable()
    {
        StartGame.OnActivatePane -= ActivatePane;
    }

    private void Awake()
    {
//        foreach (var pane in panes)
//        {
//            pane.SetActive(false);
//        }
    }

    private void ActivatePane(int paneNumber)
    {
        panes[paneNumber].SetActive(true);
    }
}
