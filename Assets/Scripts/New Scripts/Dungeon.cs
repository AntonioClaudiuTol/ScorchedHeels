using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField] private GameObject combatPanel;
    
    public void OpenDungeonMenu()
    {
        combatPanel.SetActive(!combatPanel.activeSelf);
    }
}
