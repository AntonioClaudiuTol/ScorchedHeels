using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
	Helmet,
	Chest,
	Gloves,
	Boots,
	WeaponMain,
	WeaponOff,
	Accessory1,
	Accessory2
}

[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : Item
{
	public int DamageBonus;
	public int DefenseBonus;
	public int HPRegenBonus;
	[Space]
	public float DamagePercentBonus;
	public float DefensePercentBonus;
	public float HPRegenPercentBonus;
	[Space]
	public EquipmentType EquipmentType;

	public override Item GetCopy()
	{
		return Instantiate(this);
	}

	public override void Destroy()
	{
		Destroy(this);
	}

	public void Equip(Character c)
	{
		if(DamageBonus != 0)
		{
			c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));
		}
		if (DefenseBonus != 0)
		{
			c.Defense.AddModifier(new StatModifier(DefenseBonus, StatModType.Flat, this));
		}
		if (HPRegenBonus != 0)
		{
			c.HPRegen.AddModifier(new StatModifier(HPRegenBonus, StatModType.Flat, this));
		}

		if (DamagePercentBonus != 0)
		{
			c.Damage.AddModifier(new StatModifier(DamagePercentBonus, StatModType.PercentMult, this));
		}
		if (DefensePercentBonus != 0)
		{
			c.Defense.AddModifier(new StatModifier(DefensePercentBonus, StatModType.PercentMult, this));
		}
		if (HPRegenPercentBonus != 0)
		{
			c.HPRegen.AddModifier(new StatModifier(HPRegenPercentBonus, StatModType.PercentMult, this));
		}
	}

	public void Unequip(Character c)
	{
		c.Damage.RemoveAllModifiersFromSource(this);
		c.Defense.RemoveAllModifiersFromSource(this);
		c.HPRegen.RemoveAllModifiersFromSource(this);
	}

	public override string GetItemType()
	{
		return EquipmentType.ToString();
	}

	public override string GetDescription()
	{
		sb.Length = 0;
		AddStat(DamageBonus, "Damage");
		AddStat(DefenseBonus, "Defense");
		AddStat(HPRegenBonus, "HPRegen");

		AddStat(DamagePercentBonus, "Damage", true);
		AddStat(DefensePercentBonus, "Defense", true);
		AddStat(HPRegenPercentBonus, "HPRegen", true);
		return sb.ToString();
	}

	private void AddStat(float value, string statName, bool isPercent = false)
	{
		if (value != 0)
		{
			if (sb.Length > 0)
			{
				sb.AppendLine();
			}
			if (value > 0)
			{
				sb.Append("+");
			}

			if (isPercent)
			{
				sb.Append(value * 100);
				sb.Append("% ");
			}
			else
			{
				sb.Append(value);
				sb.Append(" ");
			}
			sb.Append(statName);
		}
	}
}
