using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine;

public class Inventory : ItemContainer
{
	[SerializeField] Item[] startingItems;
	[SerializeField] Transform itemParent;
	
	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;
	public event Action<BaseItemSlot> OnBeginDragEvent;
	public event Action<BaseItemSlot> OnEndDragEvent;
	public event Action<BaseItemSlot> OnDragEvent;
	public event Action<BaseItemSlot> OnDropEvent;

	private void Start()
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			itemSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			itemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
			itemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
			itemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
			itemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
			itemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
			itemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
		}
		SetStartingItems();
	}

	private void OnValidate()
	{
		if (itemParent != null)
		{
			itemSlots = itemParent.GetComponentsInChildren<ItemSlot>();
		}
		if (!Application.isPlaying)
		{
			SetStartingItems();
		}

		foreach (ItemSlot itemslot in itemSlots)
		{
			if(itemslot.Item != null)
			Debug.Log("Item name: " + itemslot.Item.name + " - Item ID: " + itemslot.Item.ID);
		}
	}

	private void SetStartingItems()
	{
		ClearItems();

		foreach (Item item in startingItems)
		{
			AddItem(item.GetCopy());
		}
	}
}
