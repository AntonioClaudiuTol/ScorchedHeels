using UnityEngine;

public class Inventory : ItemContainer
{
	[SerializeField] Item[] startingItems;
	[SerializeField] Transform itemParent;
	
	protected override void Awake()
	{
		base.Awake();
		SetStartingItems();
	}

	protected override void OnValidate()
	{
		if (itemParent != null)
		{
			itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true, result: ItemSlots);
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
