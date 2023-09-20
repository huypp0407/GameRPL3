using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIUpgrade : _MonoBehaviour
{
    private static UIUpgrade instance;
    public static UIUpgrade Instance => instance;
    [SerializeField] protected bool isOpen = true;
    public bool IsOpen => isOpen;
    public List<ItemInventory> items;

    protected override void Awake()
    {
        base.Awake();
        if (UIUpgrade.instance != null) return;
        UIUpgrade.instance = this;
    }

    [SerializeField] protected UIUpgradeCtrl uiUpgradeCtrl;
    public UIUpgradeCtrl UIUpgradeCtrl => uiUpgradeCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIPlayerCtrl();
    }
    protected virtual void LoadUIPlayerCtrl()
    {
        if (this.uiUpgradeCtrl != null) return;
        this.uiUpgradeCtrl = transform.parent.GetComponent<UIUpgradeCtrl>();
    }

    protected override void Start()
    {
        base.Start();
        items = PlayerCtrl.Instance.Inventory.ItemsEquipment;
        this.uiUpgradeCtrl.SetAlphaCanvas(0, 0f);
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) Open();
        else this.Close();
    }

    public virtual void ShowItems()
    {
        if (!isOpen) return;

        for(int i=0; i<3; i++)
        {
            this.SpawnIngredient(i);
        }
    }

    public virtual void SpawnIngredient(int index)
    {
        int level = PlayerCtrl.Instance.Inventory.ItemsEquipment[index].upgradeLevel;
        this.uiUpgradeCtrl.UpgradeSpawner.ClearItems(index);

        List<ItemRecipeIngredient> itemsIngredient = PlayerCtrl.Instance.Inventory.ItemsEquipment[index].itemProfileSO.upgradeLevels[level].ingredients;

        for (int i = 0; i < itemsIngredient.Count; i++)
        {
            this.uiUpgradeCtrl.UpgradeSpawner.SpawnItem(itemsIngredient[i], index);
        }
    }

    public virtual void Open()
    {
        this.uiUpgradeCtrl.TextUpgradeCtrl.SetText(0);
        this.uiUpgradeCtrl.TextUpgradeCtrl.SetText(1);
        this.uiUpgradeCtrl.TextUpgradeCtrl.SetText(2);

        if (UIInform.Instance.IsOpen)
        {
            this.uiUpgradeCtrl.SetAlphaCanvas(1, 0f);
            UIInform.Instance.CloseOther();
            return;
        }

        ResizeInventory(new Vector2(700, 750), new Vector3(700, 17, 0), 1);
        this.ShowItems();
    }

    public virtual void CloseOther()
    {
        this.uiUpgradeCtrl.SetAlphaCanvas(0, 0f);
        this.isOpen = !this.isOpen;
    }

    public virtual void Close()
    {
        ResizeInventory(new Vector2(1150, 750), new Vector3(960, 17, 0), 0);
    }

    protected virtual void ResizeInventory(Vector2 size, Vector3 pos, float alpha)
    {

        RectTransform rt = UIInventoryCtrl.Instance.GetComponent<RectTransform>();
        rt.DOSizeDelta(size, 0.3f).SetUpdate(true);
        rt.DOAnchorPos3D(pos, 0.3f).SetUpdate(true);

        this.uiUpgradeCtrl.SetAlphaCanvas(alpha, 0.3f);
    }
}
