using UnityEngine;

public class InventoryInput : MonoBehaviour
{
	[SerializeField] GameObject characterPanelGameObject;
	[SerializeField] GameObject equipmentPanelGameObject;
	[SerializeField] KeyCode[] toggleCharacterPanelKeys;
	[SerializeField] KeyCode[] toggleInventoryKeys;

	void Update()
    {
		for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
		{
			if(Input.GetKeyDown(toggleCharacterPanelKeys[i]))
			{
				ToggleEquipmentPanelFull();
				break;
			}
		}

		for (int i = 0; i < toggleInventoryKeys.Length; i++)
		{
			if (Input.GetKeyDown(toggleInventoryKeys[i]))
			{
				ToggleInventory();
				break;
			}
		}
	}

	public void ToggleEquipmentPanelFull()
	{
		characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);

		if (characterPanelGameObject.activeSelf)
		{
			equipmentPanelGameObject.SetActive(true);
		}
	}

	public void ToggleInventory()
	{
		if (!characterPanelGameObject.activeSelf)
		{
			characterPanelGameObject.SetActive(true);
			equipmentPanelGameObject.SetActive(false);
		}
		else if (equipmentPanelGameObject.activeSelf)
		{
			equipmentPanelGameObject.SetActive(false);
		}
		else
		{
			characterPanelGameObject.SetActive(false);
		}
	}

	public void ToggleEquipmentPanel()
	{
		equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
	}
}
