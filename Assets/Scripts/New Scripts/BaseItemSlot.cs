using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	[SerializeField] Image Image;
	[SerializeField] Text AmountText;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;

	private Color normalColor = Color.white;
	private Color disabledColor = Color.clear;

	private Item _item;
	public Item Item
	{
		get { return _item; }
		set
		{
			_item = value;
			if (_item == null)
			{
				Image.color = disabledColor;
			}
			else
			{
				Image.sprite = _item.Icon;
				Image.color = normalColor;
			}
		}
	}

	private int _amount;
	public int Amount
	{
		get
		{
			return _amount;
		}
		set
		{
			_amount = value;
			AmountText.enabled = _item != null && _item.MaximumStacks > 1 && _amount > 1;
			if (AmountText.enabled)
			{
				AmountText.text = _amount.ToString();
			}
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
		{
			if (OnRightClickEvent != null)
			{
				OnRightClickEvent(this);
			}
		}
	}

	protected virtual void OnValidate()
	{
		if (Image == null)
		{
			Image = GetComponent<Image>();
		}
		if (AmountText == null)
		{
			AmountText = GetComponentInChildren<Text>();
		}
	}

	public virtual bool CanReceiveItem(Item item)
	{

		return false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (OnPointerEnterEvent != null)
		{
			OnPointerEnterEvent(this);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (OnPointerExitEvent != null)
		{
			OnPointerExitEvent(this);
		}
	}
}
