using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : Spawner
{
    private static CharacterSpawner instance;
    public static CharacterSpawner Instance { get => instance; }
    public static string itemNormal = "UIInvItem";

    [SerializeField] protected UICharacterCtrl characterCtrl;
    public UICharacterCtrl CharacterCtrl => characterCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (CharacterSpawner.instance != null) return;
        CharacterSpawner.instance = this;
    }

    protected override void LoadHolder()
    {
        this.LoadUICharacterCtrl();

        if (this.holder != null) return;
        this.holder = this.characterCtrl.Content;
    }

    protected virtual void LoadUICharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = transform.parent.GetComponent<UICharacterCtrl>();
    }

    public virtual void ClearItems()
    {
        foreach (Transform item in holder)
        {
            this.Despawn(item);
        }
    }

    public virtual void SpawnItem(ItemProfileSO item)
    {
        Debug.Log("SpawnItem");

        Transform uiItem = this.Spawn("UIInvItem", transform.position, transform.rotation);
        uiItem.localScale = new Vector3(1, 1, 1);
        //Debug.Log("SpawnItem");

        UIItemInventory itemInventory = uiItem.GetComponent<UIItemInventory>();
        Debug.Log(itemInventory);
        itemInventory.ShowItem(item);
    }
}