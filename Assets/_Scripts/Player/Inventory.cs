using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Inventory : _MonoBehaviour
{
    [SerializeField] protected int maxSlot = 70;
    [SerializeField] protected List<ItemInventory> items;
    public List<ItemInventory> Items => items;

    [SerializeField] protected List<ItemInventory> itemsEquipment;
    public List<ItemInventory> ItemsEquipment => itemsEquipment;
    [SerializeField] protected ItemUpgrade itemUpgrade;
    
    [SerializeField] protected int maxSlotAbilities = 4;
    [SerializeField] protected List<ItemInventory> itemsAbilities;
    public List<ItemInventory> ItemsAbilities => itemsAbilities;
    public ItemUpgrade ItemUpgrade => itemUpgrade;
    [SerializeField] protected ItemDrop itemDrop;

    [SerializeField] protected Transform SpawnButtonPos;
    [SerializeField] protected TextMeshProUGUI text;
    [SerializeField] protected CanvasGroup textCanvas;
    
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadItemUpgrade();
        this.LoadItemDrop();
    }

    protected virtual void LoadItemUpgrade()
    {
        if (this.itemUpgrade != null) return;
        this.itemUpgrade = GetComponentInChildren<ItemUpgrade>();
    }

    protected virtual void LoadItemDrop()
    {
        if (this.itemDrop != null) return;
        this.itemDrop = GetComponentInChildren<ItemDrop>();
    }

    protected override void Awake()
    {
        base.Awake();
        SetTextPize("HUY");
        this.SetUp();
    }

    protected virtual void SetUp()
    {
        this.itemUpgrade.inventory = this;
        this.itemDrop.inventory = this;
    }

    public virtual bool AddItem(ItemInventory itemInventory)
    {
        int addCount = itemInventory.itemCount;
        ItemProfileSO itemProfileSO = itemInventory.itemProfileSO;
        ItemCode itemCode = itemProfileSO.itemCode;
        ItemType itemType = itemProfileSO.itemType;

        if (itemType == ItemType.Equiment || itemType == ItemType.Clothing) return AddEquipment(itemInventory);
        if (itemType == ItemType.Ability) return AddAbility(itemCode);
        if (itemType == ItemType.Weapon) return AddWeapon(itemCode);
        if (itemType == ItemType.Prize) return AddPrize(itemCode);
        return AddItem(itemCode, addCount);
    }

    public virtual bool AddEquipment(ItemInventory itemInventory)
    {
        if (this.IsIventoryFull()) return false;
        itemInventory = itemInventory.Clone();
        this.items.Add(itemInventory);
        return true;
    }
    
    public virtual bool AddPrize(ItemCode itemCode)
    {
      switch (itemCode) {
        case ItemCode.GoodBox:
          this.UseBox(1);
          break;
        case ItemCode.BadBox:
          this.UseBox(-1);
          break;
        case ItemCode.RandomBox:
          int rnd = Random.Range(0, 1);
          this.UseBox(rnd == 0 ? -1.5f : 1.5f);
          break;
      }
      return true;
    }

   IEnumerator SetTextPize(string textPize) {
      text.SetText(textPize);
      textCanvas.DOFade(1f, 1f);
      yield return new WaitForSeconds(2f);
      textCanvas.DOFade(0, 1f);
   }
    protected void UseBox(float index) {
      int rnd = index < 0 ? Random.Range(1, 3) : Random.Range(1, 6);
      string textPize = null;
      switch (rnd) {
        case 1:
          PlayerCtrl.Instance.playerMove.moveSpeed += index * 0.5f;
          textPize = index < 0 ? "Speed Down" : "Speed Up";
          break;
        case 2:
          PlayerCtrl.Instance.PlayerDamageReceiver.Add(index * 10);
          textPize = index < 0 ? "Damage Down" : "Damage Up";
          break;
        case 3:
          AddWeapon(ItemCode.Gun);
          textPize = "Set Gun";
          break;
        case 4:
          AddAbility(ItemCode.Bomb);
          textPize = "Add Bomb";
          break;
        case 5:
          AddAbility(ItemCode.Smoke);
          textPize = "Add smoke";
          break;
      }
      StartCoroutine(SetTextPize(textPize));
    }
    
    public virtual bool AddWeapon(ItemCode itemCode)
    {
      switch (itemCode) {
        case ItemCode.Sword: 
          PlayerShooting.Instance.GetAxe();
          break; 
        case ItemCode.Boomerang:
          PlayerShooting.Instance.GetBoomerang();
          break;
        case ItemCode.Gun:
          PlayerShooting.Instance.GetGun();
          break;
      }
      return true;
    }

    // protected override void Start() {
    //   base.Start();
    //   AddAbility(ItemCode.Bomb);
    // }

    public virtual bool AddAbility(ItemCode itemCode)
    {
      ItemProfileSO itemProfileSO = ItemProfileSO.FindByItemCode(itemCode);

      int addRemain = 1;

      ItemInventory itemExist = null;
  
      foreach (ItemInventory item in itemsAbilities) {
        if(item.itemProfileSO.itemCode != itemCode) continue;
        itemExist = item;
      }
      
      if(itemExist == null) {
        if (itemsAbilities.Count >= maxSlotAbilities) return false;
        itemExist = this.CreateEmptyItem(itemProfileSO);
        GameObject button = Instantiate(itemProfileSO.ButtonPrefab);
        button.transform.SetParent(SpawnButtonPos);
        button.transform.localScale = Vector3.one;
        itemExist.button = button;
        button.name = "ItemSlot_1";
        this.itemsAbilities.Add(itemExist);
      } else {
        if (itemExist.itemCount >= itemExist.maxStack) return false;
        itemExist.button.gameObject.SetActive(true);
      }

      itemExist.itemCount++;
      SetButtonInfo(itemExist.button, itemExist.itemCount);
      return true;
    }

    protected void SetButtonInfo(GameObject button, int count) {
      button.GetComponentInChildren<TextMeshProUGUI>().SetText(count.ToString());
    }
    
    public virtual bool AddItem(ItemCode itemCode, int addCount)
    {
        ItemProfileSO itemProfileSO = ItemProfileSO.FindByItemCode(itemCode);

        int addRemain = addCount;
        int newcount;
        int itemMaxStack;
        int addMore;

        ItemInventory itemExist;

        for(int i=0; i< this.maxSlot; i++)
        {
            itemExist = this.GetItemNotFullStack(itemCode, items);
            if(itemExist == null)
            {
                if (this.IsIventoryFull()) return false;
                itemExist = this.CreateEmptyItem(itemProfileSO);
                this.items.Add(itemExist);
            }

            newcount = itemExist.itemCount + addRemain;

            itemMaxStack = this.GetMaxStack(itemExist);
            if(newcount > itemMaxStack)
            {
                addMore = itemMaxStack - itemExist.itemCount;
                newcount = itemExist.itemCount + addMore; ;
                addRemain -= addMore;
            }
            else
            {
                addRemain -= newcount;
            }

            itemExist.itemCount = newcount;
            if (addRemain < 1) break;
        }
        return true;
    }

    protected virtual bool IsIventoryFull()
    {
        if (this.items.Count >= this.maxSlot) return true;
        return false;
    }

    protected virtual int GetMaxStack(ItemInventory itemInventory)
    {
        if (itemInventory == null) return 0;
        return itemInventory.maxStack;
    }

    protected virtual ItemProfileSO GetItemProfile(ItemCode itemCode)
    {
        var profiles = Resources.LoadAll("ItemProfiles", typeof(ItemProfileSO));
        foreach (ItemProfileSO profile in profiles)
        {
            if (profile.itemCode != itemCode) continue;
            return profile;
        }
        return null;
    }

    protected virtual ItemInventory GetItemNotFullStack(ItemCode itemCode, List<ItemInventory> items)
    {
        foreach(ItemInventory itemInventory in items)
        {
            if (itemCode != itemInventory.itemProfileSO.itemCode) continue;
            if (this.IsFullStack(itemInventory)) continue;
            return itemInventory;
        }
        return null;
    }

    protected virtual bool IsFullStack(ItemInventory itemInventory)
    {
        if (itemInventory == null) return true;
        int maxStack = this.GetMaxStack(itemInventory);
        return itemInventory.itemCount >= maxStack;

    }

    protected virtual ItemInventory CreateEmptyItem(ItemProfileSO itemProfileSO)
    {
        ItemInventory itemInventory = new ItemInventory
        {
            itemProfileSO = itemProfileSO,
            maxStack = itemProfileSO.defaultMaxStack
        };

        return itemInventory;
    }

    public virtual bool ItemCheck(ItemCode itemCode, int numbeCheck)
    {
        int totalCount = this.ItemTotalCount(itemCode);
        return totalCount >= numbeCheck;
    }

    public virtual int ItemTotalCount(ItemCode itemCode)
    {
        int totalCount = 0;
        foreach(ItemInventory itemInventory in this.items)
        {
            if (itemInventory.itemProfileSO.itemCode != itemCode) continue;
            totalCount += itemInventory.itemCount;
        }

        return totalCount;
    }

    public virtual void DeductItem(ItemCode itemCode, int deductCount)
    {
        ItemInventory itemInventory;
        int deduct;

        for(int i=this.items.Count-1; i>=0; i--)
        {
            if (deductCount <= 0) break;

            itemInventory = this.items[i];
            if (itemInventory.itemProfileSO.itemCode != itemCode) continue;

            if(deductCount > itemInventory.itemCount)
            {
                deduct = itemInventory.itemCount;
                deductCount -= itemInventory.itemCount;
            }
            else
            {
                deduct = deductCount;
                deductCount = 0;
            }

            itemInventory.itemCount -= deduct;
            if (itemInventory.itemCount == 0) this.items.RemoveAt(i);
        }
    }

    public virtual void InventoryFromJson(string jsonString)
    {
        InventoryData obj = JsonUtility.FromJson<InventoryData>(jsonString);
        this.maxSlot = obj.maxSlot;
        this.items = obj.items;
    }

    public void DeductAbilities(string abiName) {
      foreach (var item in itemsAbilities) {
        if (item.itemProfileSO.itemCode.ToString() == abiName) {
          item.itemCount--;
          if (item.itemCount == 0) {
            item.button.gameObject.SetActive(false);
            PlayerShooting.Instance.SetCurrentWeapon();
          }
          SetButtonInfo(item.button, item.itemCount);
        }
      }
    }

    [SerializeField] protected GameObject btnChangePrefab;
    public GameObject btnChange;

    public void CreateBtnChange() {
      if (btnChangePrefab != null && btnChange == null) {
        btnChange = Instantiate(btnChangePrefab);
        btnChange.transform.SetParent(SpawnButtonPos);
        btnChange.transform.localScale = Vector3.one;
      }
    }
    
}
