using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

	public CharacterStat Strength;
	public CharacterStat Agility;
	public CharacterStat Intelligence;
	public CharacterStat Vitality;

	[SerializeField] Inventory inventory;
	[SerializeField] EquipmentPanel equipmentPanel;
	[SerializeField] StatPanel statPanel;
	[SerializeField] ItemTooltip itemTooltip;
	[SerializeField] Image draggableItem;
	[SerializeField] StatTooltip statTooltip;

	private BaseItemSlot dragItemSlot;

	private void OnValidate()
	{
		if(itemTooltip == null)
		{
			itemTooltip = FindObjectOfType<ItemTooltip>();
		}
	}

	private void Awake()
	{
		statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
		statPanel.UpdateStatValues();

		// Setup Events:
		// Right Click
		inventory.OnRightClickEvent += Equip;
		equipmentPanel.OnRightClickEvent += Unequip;
		// Pointer Enter
		inventory.OnPointerEnterEvent += ShowTooltip;
		equipmentPanel.OnPointerEnterEvent += ShowTooltip;
		// Pointer Enter
		inventory.OnPointerExitEvent += HideTooltip;
		equipmentPanel.OnPointerExitEvent += HideTooltip;
		// Begin Drag
		inventory.OnBeginDragEvent += BeginDrag;
		equipmentPanel.OnBeginDragEvent += BeginDrag;
		// End Drag
		inventory.OnEndDragEvent += EndDrag;
		equipmentPanel.OnEndDragEvent += EndDrag;
		// Drag
		inventory.OnDragEvent += Drag;
		equipmentPanel.OnDragEvent += Drag;
		// Drop
		inventory.OnDropEvent += Drop;
		equipmentPanel.OnDropEvent += Drop;
	}

	private void Equip(BaseItemSlot itemSlot)
	{
		EquippableItem equippableItem = itemSlot.Item as EquippableItem;
		if(equippableItem != null)
		{
			Equip(equippableItem);
		}
	}

	private void Unequip(BaseItemSlot itemSlot)
	{
		EquippableItem equippableItem = itemSlot.Item as EquippableItem;
		if (equippableItem != null)
		{
			Unequip(equippableItem);
		}
	}

	private void ShowTooltip(BaseItemSlot itemSlot)
	{
		EquippableItem equippableItem = itemSlot.Item as EquippableItem;
		if (equippableItem != null)
		{
			itemTooltip.ShowTooltip(equippableItem);
		}
	}

	private void HideTooltip(BaseItemSlot itemSlot)
	{
		itemTooltip.HideTooltip();
	}

	private void BeginDrag(BaseItemSlot itemSlot)
	{
		
		if (itemSlot != null)
		{
			dragItemSlot = itemSlot;
			draggableItem.sprite = itemSlot.Item.Icon;
			draggableItem.transform.position = Input.mousePosition;
			draggableItem.enabled = true;
			Debug.Log("Begin Drag");
		}
	}

	private void EndDrag(BaseItemSlot itemSlot)
	{
		dragItemSlot = null;
		draggableItem.enabled = false;
	}

	private void Drag(BaseItemSlot itemSlot)
	{
		if(draggableItem.enabled)
		{
			draggableItem.transform.position = Input.mousePosition;
		}
	}

	private void Drop(BaseItemSlot dropItemSlot)
	{
		if (dragItemSlot == null) return;

		if(dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
		{
			Debug.Log("after if");
			EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
			EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

			if (dragItemSlot is EquipmentSlot)
			{
				if (dragItem != null) dragItem.Unequip(this);
				if (dropItem != null) dropItem.Equip(this);
			}
			if (dropItemSlot is EquipmentSlot)
			{
				if (dragItem != null) dragItem.Equip(this);
				if (dropItem != null) dropItem.Unequip(this);
			}
			statPanel.UpdateStatValues();


			Item draggedItem = dragItemSlot.Item;
			int draggedItemAmount = dragItemSlot.Amount;

			dragItemSlot.Item = dropItemSlot.Item;
			dragItemSlot.Amount = dropItemSlot.Amount;

			dropItemSlot.Item = draggedItem;
			dropItemSlot.Amount = draggedItemAmount;
		}
	}

	public void Equip(EquippableItem item)
	{
		if (inventory.RemoveItem(item))
		{
			EquippableItem previousItem;
			if (equipmentPanel.AddItem(item, out previousItem))
			{
				if(previousItem != null)
				{
					inventory.AddItem(previousItem);
					previousItem.Unequip(this);
					statPanel.UpdateStatValues();
				}
				item.Equip(this);
				statPanel.UpdateStatValues();
			}
			else
			{
				inventory.AddItem(item);
			}
		}
	}

	public void Unequip(EquippableItem item)
	{
		if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
		{
			item.Unequip(this);
			statPanel.UpdateStatValues();
			inventory.AddItem(item);
		}
	}
}
