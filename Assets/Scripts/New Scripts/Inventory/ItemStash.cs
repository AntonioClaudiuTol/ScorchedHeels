using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStash : ItemContainer
{
	[SerializeField] Transform itemParent;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] KeyCode openKeyCode = KeyCode.E;

	private bool isOpen;
	private bool isInRange = true;

	private Character character;

	protected override void OnValidate()
	{
		//if (spriteRenderer == null)
		//{
		//	spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		//}

		if (itemParent != null)
		{
			itemParent.GetComponentsInChildren<ItemSlot>(includeInactive: true, result: ItemSlots);
		}

		//spriteRenderer.enabled = false;
	}

	protected override void Awake()
	{
		base.Awake();
		itemParent.gameObject.SetActive(false);

		character = GameObject.FindWithTag("Player").GetComponent<Character>();
	}

	private void Update()
	{
		if(Input.GetKeyDown(openKeyCode))
		{
			ToggleItemStash();
		}
	}

	public void ToggleItemStash()
	{
		isOpen = !isOpen;
		itemParent.gameObject.SetActive(isOpen);

		if (isOpen)
		{
			character.OpenItemContainer(this);
		}
		else
		{
			character.CloseItemContainer(this);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		CheckCollision(other.gameObject, true);
	}

	private void OnTriggerExit(Collider other)
	{
		CheckCollision(other.gameObject, false);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, true);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		CheckCollision(collision.gameObject, false);
	}

	private void CheckCollision(GameObject gameObject, bool state)
	{
		if (gameObject.CompareTag("Player"))
		{
			isInRange = state;
			spriteRenderer.enabled = state;
		}
	}
}
