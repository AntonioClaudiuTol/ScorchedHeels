using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPanelToggle : MonoBehaviour
{
    [SerializeField] private GameObject locationPanel;
    [SerializeField] KeyCode toggleLocationPanelWindowKeyCode = KeyCode.X;

    void Update()
    {
        if (Input.GetKeyDown(toggleLocationPanelWindowKeyCode))
        {
            ToggleLocationPanel();
        }
    }

    private void ToggleLocationPanel()
    {
        locationPanel.gameObject.SetActive(!locationPanel.gameObject.activeSelf);
    }
}
