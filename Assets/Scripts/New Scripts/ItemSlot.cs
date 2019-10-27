using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
	[SerializeField] Image Image;
	[SerializeField] Text AmountText;

	public event Action<ItemSlot> OnPointerEnterEvent;
	public event Action<ItemSlot> OnPointerExitEvent;
	public event Action<ItemSlot> OnRightClickEvent;
	public event Action<ItemSlot> OnBeginDragEvent;
	public event Action<ItemSlot> OnEndDragEvent;
	public event Action<ItemSlot> OnDragEvent;
	public event Action<ItemSlot> OnDropEvent;

	private Color normalColor = Color.white;
	private Color disabledColor = Color.clear;

	private Item _item;
	public Item Item
	{
		get { return _item; }
		set
		{
			_item = value;
			if(_item == null)
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
	public int Amount {
		get
		{
			return _amount;
		}
		set
		{
			_amount = value;
			AmountText.enabled = _item != null && _item.MaximumStacks > 1 && _amount > 1;
			if(AmountText.enabled)
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
		if(Image == null)
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

		return true;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(OnPointerEnterEvent != null)
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

	public void OnDrag(PointerEventData eventData)
	{
		if (OnDragEvent != null)
		{
			OnDragEvent(this);
		}
	}

	Vector2 originalPosition;

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (OnBeginDragEvent != null)
		{
			OnBeginDragEvent(this);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (OnEndDragEvent != null)
		{
			OnEndDragEvent(this);
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (OnDropEvent != null)
		{
			OnDropEvent(this);
		}
	}
}
