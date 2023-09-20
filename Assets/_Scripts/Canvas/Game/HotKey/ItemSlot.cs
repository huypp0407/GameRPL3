using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : _MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!UIInventory.Instance.IsOpen) return;
        GameObject dropObj = eventData.pointerDrag;
        DragItem dragItem = dropObj.GetComponent<DragItem>();
        Debug.Log(transform.name + " " + dragItem.RealParent);
        if (transform.name != dragItem.RealParent.name) return;
        if(transform.childCount > 0)
        {
            transform.GetChild(0).transform.SetParent(dragItem.RealParent);
            dragItem.SetRealParent(transform);
        }
    }
}
