using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
	[SerializeField] protected ItemSlot[] itemSlots;

	public virtual bool CanAddItem(Item item, int amount = 1)
	{
		int freeSpaces = 0;

		foreach (ItemSlot itemSlot in itemSlots)
		{
			if (itemSlot.Item == null || itemSlot.Item.ID == item.ID)
			{
				freeSpaces += item.MaximumStacks - itemSlot.Amount;
			}
		}

		return freeSpaces > amount;
	}

	public virtual bool AddItem(Item item)
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			if (itemSlots[i].CanAddStack(item))
			{
				itemSlots[i].Item = item;
				itemSlots[i].Amount++;
				return true;
			}
		}

		for (int i = 0; i < itemSlots.Length; i++)
		{
			if (itemSlots[i].Item == null)
			{
				itemSlots[i].Item = item;
				itemSlots[i].Amount++;
				return true;
			}
		}
		return false;
	}

	public virtual bool RemoveItem(Item item)
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			if (itemSlots[i].Item == item)
			{
				itemSlots[i].Amount--;
				return true;
			}
		}
		return false;
	}

	public virtual Item RemoveItem(string itemID)
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			Item item = itemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				itemSlots[i].Amount--;
				return item;
			}
		}
		return null;
	}

	public virtual bool ContainsItem(Item item)
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			if (itemSlots[i].Item == item)
			{
				return true;
			}
		}
		return false;
	}

	public virtual int ItemCount(string itemID)
	{
		int count = 0;
		for (int i = 0; i < itemSlots.Length; i++)
		{
			//if(itemSlots[i].Item == null)
			//{
			//	Debug.Log("stuff");
			//}
			//if (itemSlots[i].Item.ID == itemID)
			//{
			//	count += itemSlots[i].Amount;
			//}

			Item item = itemSlots[i].Item;
			if (item != null && item.ID == itemID)
			{
				count += itemSlots[i].Amount;
			}
			else if (item == null)
			{
				Debug.LogWarning("Item is null");
				Debug.LogWarning("Item id is - " + itemID);
			}
		}
		return count;
	}

	public virtual void ClearItems()
	{
		for (int i = 0; i < itemSlots.Length; i++)
		{
			itemSlots[i].Item = null;
		}
	}
}
