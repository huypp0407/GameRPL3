using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ItemUpgrade : _MonoBehaviour
{ 
    public Inventory inventory;

    [SerializeField] protected int maxLevel = 9;

    public virtual bool UpgradeItem(int itemIndex)
    {
        if (itemIndex >= this.inventory.ItemsEquipment.Count) return false;

        ItemInventory itemInventory = this.inventory.ItemsEquipment[itemIndex];
        if (itemInventory.upgradeLevel == maxLevel) return false;
        //if (itemInventory.itemCount < 1) return false;

        List<ItemRecipe> upgradeLevels = itemInventory.itemProfileSO.upgradeLevels;

        if (!this.ItemUpgradeable(upgradeLevels)) return false;
        if (!this.HaveEnoughIngredients(upgradeLevels, itemInventory.upgradeLevel)) return false;

        this.DeductIngredients(upgradeLevels, itemInventory.upgradeLevel);
        itemInventory.upgradeLevel++;
        UIInventoryCtrl.Instance.UIInventory.ShowItems();
        return true;
    }

    protected virtual bool ItemUpgradeable(List<ItemRecipe> upgradeLevels)
    {
        if (upgradeLevels.Count == 0) return false;
        return true; 
    }

    protected virtual bool HaveEnoughIngredients(List<ItemRecipe> upgradeLevels, int currentLevel)
    {
        ItemCode itemCode;
        int itemCount;

        if(currentLevel > upgradeLevels.Count)
        {
            Debug.Log("Can't upgrade level" + (currentLevel + 1));
            return false;
        }

        ItemRecipe currentRecipeLevel = upgradeLevels[currentLevel];
        foreach(ItemRecipeIngredient ingredient in currentRecipeLevel.ingredients)
        {
            itemCode = ingredient.itemProfileSO.itemCode;
            itemCount = ingredient.itemCount;

            if (!this.inventory.ItemCheck(itemCode, itemCount)) return false;
        }

        return true;
    }

    protected virtual void DeductIngredients(List<ItemRecipe> upgradeLevels, int currentLevel)
    {
        ItemCode itemCode;
        int itemCount;

        ItemRecipe currentRecipeLevel = upgradeLevels[currentLevel];
        foreach(ItemRecipeIngredient ingredient in currentRecipeLevel.ingredients)
        {
            itemCode = ingredient.itemProfileSO.itemCode;
            itemCount = ingredient.itemCount;

            this.inventory.DeductItem(itemCode, itemCount);
        }
    }
}
