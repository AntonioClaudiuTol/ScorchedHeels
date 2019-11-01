using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemArea : MonoBehaviour, IDropHandler
{
	public Action OndropEvent;

	public void OnDrop(PointerEventData eventData)
	{
		if(OndropEvent != null)
		{
			OndropEvent();
		}
	}
}
