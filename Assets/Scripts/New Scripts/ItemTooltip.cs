using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemTooltip : MonoBehaviour
{
	[SerializeField] Text ItemNameText;
	[SerializeField] Text ItemSlotText;
	[SerializeField] Text ItemStatsText;

	private StringBuilder sb = new StringBuilder();

	public void ShowTooltip(EquippableItem item)
	{
		ItemNameText.text = item.Name;
		ItemSlotText.text = item.EquipmentType.ToString();

		sb.Length = 0;
		AddStat(item.StrengthBonus, "Strength");
		AddStat(item.AgilityBonus, "Agility");
		AddStat(item.IntelligenceBonus, "Intelligence");
		AddStat(item.VitalityBonus, "Vitality");

		AddStat(item.StrengthPercentBonus, "Strength", true);
		AddStat(item.AgilityPercentBonus, "Agility", true);
		AddStat(item.IntelligencePercentBonus, "Intelligence", true);
		AddStat(item.VitalityPercentBonus, "Vitality", true);

		ItemStatsText.text = sb.ToString();

		gameObject.SetActive(true);
	}

	public void HideTooltip()
	{
		gameObject.SetActive(false);
	}

	private void AddStat(float value, string statName, bool isPercent = false)
	{
		if(value != 0)
		{
			if(sb.Length > 0)
			{
				sb.AppendLine();
			}
			if(value > 0)
			{
				sb.Append("+");
			}

			if(isPercent)
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
