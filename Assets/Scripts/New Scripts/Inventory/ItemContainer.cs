using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
	public List<ItemSlot> ItemSlots;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;
	public event Action<BaseItemSlot> OnBeginDragEvent;
	public event Action<BaseItemSlot> OnEndDragEvent;
	public event Action<BaseItemSlot> OnDragEvent;
	public event Action<BaseItemSlot> OnDropEvent;

	protected virtual void OnValidate()
	{
		GetComponentsInChildren<ItemSlot>(includeInactive: true, result: ItemSlots);
	}

	protected virtual void Awake()
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			ItemSlots[i].OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
			ItemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
			ItemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
			ItemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
			ItemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
			ItemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
			ItemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
		}
	}

	public virtual bool CanAddItem(Item item, int amount = 1)
	{
		int freeSpaces = 0;

		foreach (ItemSlot itemSlot in ItemSlots)
		{
			if (itemSlot.Item == null || itemSlot.Item.ID == item.ID)
			{
				freeSpaces += item.MaximumStacks - itemSlot.Amount;
			}
		}

		return freeSpaces >= amount;
	}

	public virtual bool AddItem(Item item)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].CanAddStack(item))
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount++;
				return true;
			}
		}

		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item == null)
			{
				ItemSlots[i].Item = item;
				ItemSlots[i].Amount++;
				return true;
			}
		}
		return false;
	}

	public virtual bool RemoveItem(Item item)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item == item)
			{
				ItemSlots[i].Amount--;
				return true;
			}
		}
		return false;
	}

	public virtual Item RemoveItem(string itemID)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			Item item = ItemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				ItemSlots[i].Amount--;
				return item;
			}
		}
		return null;
	}

	public virtual bool ContainsItem(Item item)
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			if (ItemSlots[i].Item == item)
			{
				return true;
			}
		}
		return false;
	}

	public virtual int ItemCount(string itemID)
	{
		int count = 0;
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			Item item = ItemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				count += ItemSlots[i].Amount;
			}
		}
		return count;
	}

	public virtual void ClearItems()
	{
		for (int i = 0; i < ItemSlots.Count; i++)
		{
			ItemSlots[i].Item = null;
		}
	}
}
