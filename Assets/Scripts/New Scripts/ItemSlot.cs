using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : BaseItemSlot, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
	public event Action<BaseItemSlot> OnBeginDragEvent;
	public event Action<BaseItemSlot> OnEndDragEvent;
	public event Action<BaseItemSlot> OnDragEvent;
	public event Action<BaseItemSlot> OnDropEvent;
	

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
