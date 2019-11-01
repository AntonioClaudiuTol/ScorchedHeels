using UnityEngine;

public class Inventory : ItemContainer
{
	[SerializeField] Item[] startingItems;
	[SerializeField] Transform itemParent;
	
	protected override void Start()
	{
		base.Start();
		SetStartingItems();
	}

	protected override void OnValidate()
	{
		if (itemParent != null)
		{
			itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true, result: itemSlots);
		}

		if (!Application.isPlaying)
		{
			SetStartingItems();
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
