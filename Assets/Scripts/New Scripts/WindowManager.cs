using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private Button explorationButton;
    [SerializeField] private Button dungeonButton;

    public static bool isExplorationUnlocked;
    public static bool isDungeonUnlocked;

    private void Update()
    {
        if (isExplorationUnlocked)
        {
            explorationButton.gameObject.SetActive(true);
        }

        if (isDungeonUnlocked)
        {
            dungeonButton.gameObject.SetActive(true);
        }
    }
}
