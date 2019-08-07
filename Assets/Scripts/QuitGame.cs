using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void OnMouseUp()
    {
        Debug.Log("entered");
        Application.Quit();
    }
}