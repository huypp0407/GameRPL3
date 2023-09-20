using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemInventory : _MonoBehaviour
{
    public ItemInventory itemInventory;

    [SerializeField] protected Text itemName;
    public Text ItemName => itemName;

    [SerializeField] protected Text itemCount;
    public Text ItemCount => itemCount;

    [SerializeField] protected Image  itemImage;
    public Image ItemImage => itemImage;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadItemName();
        this.LoadItemImage();
        this.LoadItemCount();
    }

    protected virtual void LoadItemName()
    {
        if (this.itemName != null) return;
        this.itemName = transform.Find("ItemName").GetComponent<Text>();
    }

    protected virtual void LoadItemCount()
    {
        if (this.itemCount != null) return;
        this.itemCount = transform.Find("ItemCount").GetComponent<Text>();
    }

    protected virtual void LoadItemImage()
    {
        if (this.itemImage != null) return;
        this.itemImage = transform.Find("ItemImage").GetComponent<Image>();
    }

    public virtual void ShowItem(ItemInventory item)
    {
        this.itemInventory = item;
        this.itemName.text = this.itemInventory.itemProfileSO.itemName;
        this.itemCount.text = this.itemInventory.itemCount.ToString();
        this.itemImage.sprite = this.itemInventory.itemProfileSO.sprite;
    }

    public virtual void ShowItem(ItemDropRate item)
    {
        this.itemName.text = item.itemSO.itemName;
        if (item.itemSO.itemType == ItemType.Clothing) this.itemCount.text = "1";
        else this.itemCount.text = Random.Range(10,20).ToString();
        this.itemImage.sprite = item.itemSO.sprite;
    }

    public virtual void ShowItem(ItemProfileSO item)
    {
        this.itemName.text = item.itemName;
        this.itemCount.text = "";
        this.itemImage.sprite = item.sprite;
    }

    public virtual void ShowItem(UIItemInventory item)
    {
        this.itemName.text = item.ItemName.text.ToString();
        this.itemCount.text = item.ItemCount.text.ToString();
        this.itemImage.sprite = item.ItemImage.sprite;
    }
}
