using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
	public int Health = 50;

	public CharacterStat Strength;
	public CharacterStat Agility;
	public CharacterStat Intelligence;
	public CharacterStat Vitality;

	[SerializeField] Inventory inventory;
	[SerializeField] EquipmentPanel equipmentPanel;
	[SerializeField] CraftingWindow craftingWindow;
	[SerializeField] StatPanel statPanel;
	[SerializeField] ItemTooltip itemTooltip;
	[SerializeField] Image draggableItem;

	private BaseItemSlot dragItemSlot;

	private void OnValidate()
	{
		if(itemTooltip == null)
		{
			itemTooltip = FindObjectOfType<ItemTooltip>();
		}
	}

	private void Start()
	{
		if(itemTooltip == null)
		{
			itemTooltip = FindObjectOfType<ItemTooltip>();
		}

		statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
		statPanel.UpdateStatValues();

		// Setup Events:
		// Right Click
		inventory.OnRightClickEvent += InventoryItemRightClickAction;
		equipmentPanel.OnRightClickEvent += EquipmentPanelRightClickAction;
		// Pointer Enter
		inventory.OnPointerEnterEvent += ShowTooltip;
		equipmentPanel.OnPointerEnterEvent += ShowTooltip;
		craftingWindow.OnPointerEnterEvent += ShowTooltip;
		// Pointer Exit
		inventory.OnPointerExitEvent += HideTooltip;
		equipmentPanel.OnPointerExitEvent += HideTooltip;
		craftingWindow.OnPointerExitEvent += HideTooltip;
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

	private void InventoryItemRightClickAction(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item is EquippableItem)
		{
			Equip((EquippableItem)itemSlot.Item);
		}
		else if (itemSlot.Item is UsableItem)
		{
			UsableItem usableItem = (UsableItem)itemSlot.Item;
			usableItem.Use(this);

			if(usableItem.IsConsumable)
			{
				inventory.RemoveItem(usableItem);
				usableItem.Destroy();
			}
		}
	}

	private void EquipmentPanelRightClickAction(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item is EquippableItem)
		{
			Unequip((EquippableItem)itemSlot.Item);
		}
	}

	private void ShowTooltip(BaseItemSlot itemSlot)
	{
		if (itemSlot.Item != null)
		{
			itemTooltip.ShowTooltip(itemSlot.Item);
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
			Debug.Log("BeginDrag");
			dragItemSlot = itemSlot;
			draggableItem.sprite = itemSlot.Item.Icon;
			draggableItem.transform.position = Input.mousePosition;
			draggableItem.enabled = true;
		}
	}

	private void EndDrag(BaseItemSlot itemSlot)
	{
		Debug.Log("EndDrag");
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

		if (dropItemSlot.CanAddStack(dragItemSlot.Item))
		{
			AddStacks(dropItemSlot);
		}
		else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
		{
			SwapItems(dropItemSlot);
		}
	}

	private void SwapItems(BaseItemSlot dropItemSlot)
	{
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

	private void AddStacks(BaseItemSlot dropItemSlot)
	{
		int numAddableStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
		int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

		dropItemSlot.Amount += stacksToAdd;
		dragItemSlot.Amount -= stacksToAdd;
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
		if (inventory.CanAddItem(item) && equipmentPanel.RemoveItem(item))
		{
			item.Unequip(this);
			statPanel.UpdateStatValues();
			inventory.AddItem(item);
		}
	}

	public void UpdateStatValues()
	{
		statPanel.UpdateStatValues();
	}

	private ItemContainer openItemContainer;

	private void TransferToItemContainer(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && openItemContainer.CanAddItem(item))
		{
			inventory.RemoveItem(item);
			openItemContainer.AddItem(item);
		}
	}

	private void TransferToInventory(BaseItemSlot itemSlot)
	{
		Item item = itemSlot.Item;
		if (item != null && inventory.CanAddItem(item))
		{
			openItemContainer.RemoveItem(item);
			inventory.AddItem(item);
		}
	}

	public void OpenItemContainer(ItemContainer	itemContainer)
	{
		openItemContainer = itemContainer;

		inventory.OnRightClickEvent -= InventoryItemRightClickAction;
		inventory.OnRightClickEvent += TransferToItemContainer;

		itemContainer.OnRightClickEvent += TransferToInventory;

		itemContainer.OnPointerEnterEvent += ShowTooltip;
		itemContainer.OnPointerExitEvent += HideTooltip;
		itemContainer.OnBeginDragEvent += BeginDrag;
		itemContainer.OnEndDragEvent += EndDrag;
		itemContainer.OnDragEvent += Drag;
		itemContainer.OnDropEvent += Drop;
	}

	public void CloseItemContainer(ItemContainer itemContainer)
	{
		openItemContainer = null;

		inventory.OnRightClickEvent += InventoryItemRightClickAction;
		inventory.OnRightClickEvent -= TransferToItemContainer;

		itemContainer.OnRightClickEvent -= TransferToInventory;

		itemContainer.OnPointerEnterEvent -= ShowTooltip;
		itemContainer.OnPointerExitEvent -= HideTooltip;
		itemContainer.OnBeginDragEvent -= BeginDrag;
		itemContainer.OnEndDragEvent -= EndDrag;
		itemContainer.OnDragEvent -= Drag;
		itemContainer.OnDropEvent -= Drop;
	}
}
