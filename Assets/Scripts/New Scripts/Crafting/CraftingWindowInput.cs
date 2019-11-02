using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindowInput : MonoBehaviour
{
	[SerializeField] CraftingWindow craftingWindow;
	[SerializeField] KeyCode openCraftingWindowKeyCode = KeyCode.P;

    void Update()
    {
		if (Input.GetKeyDown(openCraftingWindowKeyCode))
		{
			craftingWindow.gameObject.SetActive(!craftingWindow.gameObject.activeSelf);
		}
	}
}
