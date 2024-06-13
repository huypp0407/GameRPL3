using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonBom : _MonoBehaviour
{
    private static UIButtonBom instance;
    public static UIButtonBom Instance => instance;

    [SerializeField] protected UIButtonBoomCtrl uiButtonBoomCtrl;

    [SerializeField] protected bool isOpen = true;
    public bool IsOpen => isOpen;

    protected override void Awake()
    {
        base.Awake();
        if (UIButtonBom.instance != null) return;
        UIButtonBom.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.uiButtonBoomCtrl != null) return;
        this.uiButtonBoomCtrl = transform.parent.GetComponent<UIButtonBoomCtrl>();
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
        this.uiButtonBoomCtrl.SetAlphaCanvas(1);
    }

    public virtual void Close()
    {
        this.uiButtonBoomCtrl.SetAlphaCanvas(0);
    }
}
