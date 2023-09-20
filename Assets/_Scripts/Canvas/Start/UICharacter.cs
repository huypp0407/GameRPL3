using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UICharacter : _MonoBehaviour
{
    private static UICharacter instance;
    public static UICharacter Instance => instance;

    [SerializeField] protected UICharacterCtrl characterCtrl;
    public UICharacterCtrl CharacterCtrl => characterCtrl;

    [SerializeField] protected bool isOpen = true;
    public bool IsOpen => isOpen;
    [SerializeField] protected InventorySort inventorySort = InventorySort.SortByName;

    protected override void Awake()
    {
        base.Awake();
        if (UICharacter.instance != null) return;
        UICharacter.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUICharacterCtrl();
    }

    protected virtual void LoadUICharacterCtrl()
    {
        if (this.characterCtrl != null) return;
        this.characterCtrl = transform.parent.GetComponent<UICharacterCtrl>();
    }

    protected override void Start()
    {
        base.Start();
        this.Toggle();
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) Open();
        else this.Close();

    }

    public virtual void Open()
    {
        RectTransform rt = this.characterCtrl.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(0, 0, 0);
        this.characterCtrl.SetAlphaCanvas(1);
        this.ShowItems();
    }

    public virtual void Close()
    {
        this.characterCtrl.SetAlphaCanvas(0);
    }

    public virtual void ShowItems()
    {
        if (!isOpen) return;

        this.ClearItems();

        List<ItemProfileSO> items = Character.Instance.Items;
        CharacterSpawner spawner = this.characterCtrl.CharacterSpawner;
        Debug.Log(spawner);
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i]);

            spawner.SpawnItem(items[i]);
        }
    }

    protected virtual void ClearItems()
    {
        this.characterCtrl.CharacterSpawner.ClearItems();
    }
}
