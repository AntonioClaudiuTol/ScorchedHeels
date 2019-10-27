﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] Image Image;
	[SerializeField] ItemTooltip tooltip;

	public event Action<Item> OnRightClickEvent;

	private Item item;
	public Item Item
	{
		get { return item; }
		set
		{
			item = value;
			if(item == null)
			{
				Image.enabled = false;
			}
			else
			{
				Image.sprite = item.Icon;
				Image.enabled = true;
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
		{
			if (Item != null && OnRightClickEvent != null)
			{
				OnRightClickEvent(Item);
			}
		}
	}

	protected virtual void OnValidate()
	{
		if(Image == null)
		{
			Image = GetComponent<Image>();
		}

		if(tooltip == null)
		{
			tooltip = FindObjectOfType<ItemTooltip>();
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(Item is EquippableItem)
		{
			tooltip.ShowTooltip((EquippableItem)Item);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.HideTooltip();
	}
}
