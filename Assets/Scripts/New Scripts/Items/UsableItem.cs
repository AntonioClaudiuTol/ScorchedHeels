using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Usable Item")]
public class UsableItem : Item
{
	public bool IsConsumable;
	public List<UsableItemEffect> usableItemEffects;

	public virtual void Use(Character character)
	{
		foreach (UsableItemEffect usableItemEffect in usableItemEffects)
		{
			usableItemEffect.ExecuteEffect(this, character);
		}
	}

	public override string GetItemType()
	{
		return IsConsumable ? "Consumable" : "Usable";
	}

	public override string GetDescription()
	{
		sb.Length = 0;

		foreach (UsableItemEffect itemEffect in usableItemEffects)
		{
			sb.AppendLine(itemEffect.GetDescription());
		}

		return sb.ToString();
	}
}
