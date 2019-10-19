using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CraftingButton : MonoBehaviour
{
    public GameObject craftingPanel;
    public void OpenCraftingPanel()
    {
        craftingPanel.SetActive(!craftingPanel.activeSelf);
    }
}
