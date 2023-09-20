using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIInform : _MonoBehaviour
{
    private static UIInform instance;
    public static UIInform Instance => instance;
    [SerializeField] protected bool isOpen = false;
    public bool IsOpen => isOpen;

    protected override void Awake()
    {
        base.Awake();
        if (UIInform.instance != null) return;
        UIInform.instance = this;
    }

    [SerializeField] protected UIPlayerCtrl uiPlayerCtrl;
    public UIPlayerCtrl UIPlayerCtrl => uiPlayerCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIPlayerCtrl();
    }
    protected virtual void LoadUIPlayerCtrl()
    {
        if (this.uiPlayerCtrl != null) return;
        this.uiPlayerCtrl = transform.parent.GetComponent<UIPlayerCtrl>();
    }
    protected override void Start()
    {
        base.Start();
        this.uiPlayerCtrl.SetAlphaCanvas(0, 0f);
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) Open();
        else this.Close();
    }

    public virtual void Open()
    {
        this.uiPlayerCtrl.TextInformCtrl.SetText();

        if (UIUpgrade.Instance.IsOpen)
        {
            this.uiPlayerCtrl.SetAlphaCanvas(1, 0f);
            UIUpgrade.Instance.CloseOther();
            return;
        }
        ResizeInventory(new Vector2(700, 750), new Vector3(700, 17, 0), 1);
    }

    public virtual void CloseOther()
    {
        this.uiPlayerCtrl.SetAlphaCanvas(0, 0f);
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

        this.uiPlayerCtrl.SetAlphaCanvas(alpha, 0.3f);
    }
}
