using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	[SerializeField] protected Image Image;
	[SerializeField] protected Text AmountText;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;

	protected bool isPointerOver;

	protected Color normalColor = Color.white;
	protected Color disabledColor = Color.clear;

	[SerializeField] protected Item _item;
	public Item Item
	{
		get { return _item; }
		set
		{
			_item = value;
			if (_item == null && Amount != 0)
			{
				Amount = 0;
			}

			if (_item == null)
			{
				Image.color = disabledColor;
			}
			else
			{
				Image.sprite = _item.Icon;
				Image.color = normalColor;
			}

			if(isPointerOver)
			{
				OnPointerExit(null);
				OnPointerEnter(null);
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
			if(_amount < 0)
			{
				_amount = 0;
			}
			if (_amount == 0 && Item != null)
			{
				Item = null;
			}

			if(AmountText != null)
			{
				AmountText.enabled = _item != null && _amount > 1;
				if (AmountText.enabled)
				{
					AmountText.text = _amount.ToString();
				}
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

	protected virtual void OnDisable()
	{
		if(isPointerOver)
		{
			OnPointerExit(null);
		}
	}

	public virtual bool CanAddStack(Item item, int amount = 1)
	{
		return Item != null && Item.ID == item.ID;
	}

	public virtual bool CanReceiveItem(Item item)
	{
		return false;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true; 

		if (OnPointerEnterEvent != null)
		{
			OnPointerEnterEvent(this);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;
		if (OnPointerExitEvent != null)
		{
			OnPointerExitEvent(this);
		}
	}
}
