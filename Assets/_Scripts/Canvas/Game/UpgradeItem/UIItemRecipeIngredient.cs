using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemRecipeIngredient : _MonoBehaviour
{
    [SerializeField] protected ItemRecipeIngredient itemRecipeIngredient;
    public ItemRecipeIngredient ItemRecipeIngredient => itemRecipeIngredient;

    [SerializeField] protected Text itemCount;
    public Text ItemCount => itemCount;

    [SerializeField] protected Image  itemImage;
    public Image ItemImage => itemImage;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadItemImage();
        this.LoadItemCount();
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

    public virtual void ShowItem(ItemRecipeIngredient item)
    {
        this.itemRecipeIngredient = item;
        this.itemCount.text = this.itemRecipeIngredient.itemCount + "x ";
        this.itemImage.sprite = this.itemRecipeIngredient.itemProfileSO.sprite;
    }
}
