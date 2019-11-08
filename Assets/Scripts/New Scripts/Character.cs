using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Character : MonoBehaviour
{
    public int Health = 100;

    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Vitality;

    public Inventory Inventory;
    public EquipmentPanel EquipmentPanel;
    [SerializeField] CraftingWindow craftingWindow;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog questionDialog;
    [SerializeField] ItemSaveManager itemSaveManager;

    private BaseItemSlot dragItemSlot;
    private ItemContainer openItemContainer;

    public delegate void ReportDamageDealt(string damage);
    public static event ReportDamageDealt OnDamageDealt;

    public delegate void DealDamage(int damage);

    public static event DealDamage OnDealDamage;
    
    public delegate void CharacterDeath();

    public static event CharacterDeath OnDeath;

    private void OnValidate()
    {
        if (itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        if (itemTooltip == null)
        {
            itemTooltip = FindObjectOfType<ItemTooltip>();
        }

        statPanel.SetStats(Strength, Agility, Intelligence, Vitality);
        statPanel.UpdateStatValues();

        // Setup Events:
        // Right Click
        Inventory.OnRightClickEvent += InventoryItemRightClickAction;
        EquipmentPanel.OnRightClickEvent += EquipmentPanelRightClickAction;
        // Pointer Enter
        Inventory.OnPointerEnterEvent += ShowTooltip;
        EquipmentPanel.OnPointerEnterEvent += ShowTooltip;
        craftingWindow.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        Inventory.OnPointerExitEvent += HideTooltip;
        EquipmentPanel.OnPointerExitEvent += HideTooltip;
        craftingWindow.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        Inventory.OnBeginDragEvent += BeginDrag;
        EquipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        Inventory.OnEndDragEvent += EndDrag;
        EquipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        Inventory.OnDragEvent += Drag;
        EquipmentPanel.OnDragEvent += Drag;
        // Drop
        Inventory.OnDropEvent += Drop;
        EquipmentPanel.OnDropEvent += Drop;
        dropItemArea.OndropEvent += DropItemFromInventory;

        itemSaveManager.LoadEquipment(this);
        itemSaveManager.LoadInventory(this);

        Inventory.ClearItems();
        InitiateCombat();
    }

    


    private void OnDestroy()
    {
        itemSaveManager.SaveEquipment(this);
        itemSaveManager.SaveInventory(this);
    }

    private void InventoryItemRightClickAction(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem) itemSlot.Item);
        }
        else if (itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem) itemSlot.Item;
            usableItem.Use(this);

            if (usableItem.IsConsumable)
            {
                Inventory.RemoveItem(usableItem);
                usableItem.Destroy();
            }
        }
    }

    private void EquipmentPanelRightClickAction(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Unequip((EquippableItem) itemSlot.Item);
        }
    }

    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            itemTooltip.ShowTooltip(itemSlot.Item);
        }
    }

    private void HideTooltip(BaseItemSlot itemSlot)
    {
        itemTooltip.HideTooltip();
    }

    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot != null)
        {
            Debug.Log("BeginDrag");
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void EndDrag(BaseItemSlot itemSlot)
    {
        Debug.Log("EndDrag");
        dragItemSlot = null;
        draggableItem.enabled = false;
    }

    private void Drag(BaseItemSlot itemSlot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }

    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        if (dropItemSlot.CanAddStack(dragItemSlot.Item))
        {
            AddStacks(dropItemSlot);
        }
        else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }
    }

    private void DropItemFromInventory()
    {
        if (dragItemSlot == null)
        {
            return;
        }

        questionDialog.Show();
        BaseItemSlot baseItemSlot = dragItemSlot;
        questionDialog.OnYesEvent += () => DestroyItemInSlot(baseItemSlot);
    }

    private void DestroyItemInSlot(BaseItemSlot baseItemSlot)
    {
        baseItemSlot.Item.Destroy();
        baseItemSlot.Item = null;
    }

    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if (dragItemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Unequip(this);
            if (dropItem != null) dropItem.Equip(this);
        }

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragItem != null) dragItem.Equip(this);
            if (dropItem != null) dropItem.Unequip(this);
        }

        statPanel.UpdateStatValues();


        Item draggedItem = dragItemSlot.Item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.Item = dropItemSlot.Item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void AddStacks(BaseItemSlot dropItemSlot)
    {
        int numAddableStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddableStacks, dragItemSlot.Amount);

        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }

    public void Equip(EquippableItem item)
    {
        if (Inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (EquipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }

                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                Inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        if (Inventory.CanAddItem(item) && EquipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            Inventory.AddItem(item);
        }
    }

    public void UpdateStatValues()
    {
        statPanel.UpdateStatValues();
    }

    private void TransferToItemContainer(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && openItemContainer.CanAddItem(item))
        {
            Inventory.RemoveItem(item);
            openItemContainer.AddItem(item);
        }
    }

    private void TransferToInventory(BaseItemSlot itemSlot)
    {
        Item item = itemSlot.Item;
        if (item != null && Inventory.CanAddItem(item))
        {
            openItemContainer.RemoveItem(item);
            Inventory.AddItem(item);
        }
    }

    public void OpenItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = itemContainer;

        Inventory.OnRightClickEvent -= InventoryItemRightClickAction;
        Inventory.OnRightClickEvent += TransferToItemContainer;

        itemContainer.OnRightClickEvent += TransferToInventory;

        itemContainer.OnPointerEnterEvent += ShowTooltip;
        itemContainer.OnPointerExitEvent += HideTooltip;
        itemContainer.OnBeginDragEvent += BeginDrag;
        itemContainer.OnEndDragEvent += EndDrag;
        itemContainer.OnDragEvent += Drag;
        itemContainer.OnDropEvent += Drop;
    }

    public void CloseItemContainer(ItemContainer itemContainer)
    {
        openItemContainer = null;

        Inventory.OnRightClickEvent += InventoryItemRightClickAction;
        Inventory.OnRightClickEvent -= TransferToItemContainer;

        itemContainer.OnRightClickEvent -= TransferToInventory;

        itemContainer.OnPointerEnterEvent -= ShowTooltip;
        itemContainer.OnPointerExitEvent -= HideTooltip;
        itemContainer.OnBeginDragEvent -= BeginDrag;
        itemContainer.OnEndDragEvent -= EndDrag;
        itemContainer.OnDragEvent -= Drag;
        itemContainer.OnDropEvent -= Drop;
    }

    public int damage = 15;

//    public void DealDamage(int amount)
//    {
//        target.TakeDamage(damage);
//    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;

        if (Health <= 0)
        {
            Die();
        }
    }


    [SerializeField] Enemy enemy;

    private void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }

        combatState = CombatState.Stopped;
        CombatLog.LogCombatEventStatic("Player Died.");
    }

    private void Update()
    {
//        Attack();
        if (target == null)
        {
            FindNextTarget();
        }
//        else
//        {
//            Attack();
//        }
//        combatState = CombatState.Stopped;
        if (combatState == CombatState.Started)
        {
            Attack();
        }
        
    
//        Regenerate(20);
    }

    private float regenCD = 0.5f;
    private float currentRegenTimer = 0f;
    private bool startRegen = false;
    public int maxHealth = 100;

    private void Regenerate(int i)
    {
        if (Health <= 0)
        {
            startRegen = true;
        }

        if (startRegen)
        {
            currentRegenTimer += Time.deltaTime;
            if (currentRegenTimer >= regenCD)
            {
                Health += i;
                currentRegenTimer = 0;
                if (Health > maxHealth)
                {
                    startRegen = false;
                }
            }
        }
    }

    private float attackCooldown = 0f;
    private float attackSpeed = 0.5f;

    public enum CombatState
    {
        Started,
        Stopped
    }

    public CombatState combatState = CombatState.Stopped;

    private void Attack()
    {
        attackCooldown += Time.deltaTime;
        
        if(attackCooldown >= attackSpeed)
        {
            if (OnDamageDealt != null)
            {
                OnDamageDealt("<color=blue>" + this.name + "</color> has dealt <color=red>" + damage.ToString() + "</color> damage to <color=yellow> some enemy" + "</color>.");
            }

            if (OnDealDamage != null)
            {
                OnDealDamage(damage);
            }
//            target.TakeDamage(damage);
            attackCooldown = 0f;
//            if (target.currentHealth <= 0)
//            {
//                Debug.Log("enemy died");
////                CombatManager.RemoveEnemy(target);
//                FindNextTarget();
//            }
        }
        
//        StartCombat();
    }

//    IEnumerator Combat()
//    {
//        while (target.currentHealth > 0) /* enemy is alive*/
//        {
//            target.TakeDamage(damage);
//            yield return waitForSeconds;
//        }
//        if (target.currentHealth < 0)
//        {
//            StopAllCoroutines();
//        }
//    }
//
//    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    public Enemy target;

    public void StartCombat()
    {
//        StartCoroutine(Combat());

        
    }

    private List<Enemy> enemies;

    private void InitiateCombat()
    {
//        target = CombatManager.firstenemy;
//
//        Attack();
    }

    private void FindNextTarget()
    {
//        if (CombatManager.GetNextEnemy() != null)
//        {
//            target = CombatManager.GetNextEnemy();
//        }
    }
    


//    launch combat area -> acquire all enemies -> select first enemy -> attack until dead -> select next enemy
}