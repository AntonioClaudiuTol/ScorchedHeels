using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Locations : MonoBehaviour
{
    public GameObject locationPanel;

    public void OpenLocations()
    {
        Debug.Log("open");
        locationPanel.SetActive(!locationPanel.activeSelf);
    }
}
