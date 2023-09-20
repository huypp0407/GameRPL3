using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem : _MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] protected Transform realParent;
    public Transform RealParent => realParent;    
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected Image image;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRectTransform();
        this.LoadCanvas();
        this.LoadCanvasGroup();
        this.LoadImage();
    }

    protected virtual void LoadRectTransform()
    {
        if (this.rectTransform != null) return;
        this.rectTransform = GetComponent<RectTransform>();
    }

    protected virtual void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = transform.GetComponentInParent<Canvas>();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void LoadImage()
    {
        if (this.image != null) return;
        this.image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(UIInventory.Instance.IsOpen);
        if (!UIInventory.Instance.IsOpen) return;
        canvasGroup.alpha = 0.6f;
        this.realParent = transform.parent;
        transform.SetParent(UIHotKeyCtrl.Instance.transform);
        this.canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!UIInventory.Instance.IsOpen) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!UIInventory.Instance.IsOpen) return;
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        this.canvasGroup.blocksRaycasts = true;
        transform.SetParent(this.realParent);
    }

    public virtual void SetRealParent(Transform transform)
    {
        Debug.Log("SetRealParent");
        this.realParent = transform;
    }

}
