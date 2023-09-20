using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOption : _MonoBehaviour
{
    private static UIOption instance;
    public static UIOption Instance => instance;

    [SerializeField] protected UIOptionCtrl optionCtrl;

    [SerializeField] protected bool isOpen = true;
    public bool IsOpen => isOpen;

    protected override void Awake()
    {
        base.Awake();
        if (UIOption.instance != null) return;
        UIOption.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.optionCtrl != null) return;
        this.optionCtrl = transform.parent.GetComponent<UIOptionCtrl>();
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
        this.optionCtrl.SetAlphaCanvas(1);
    }

    public virtual void Close()
    {
        this.optionCtrl.SetAlphaCanvas(0);
    }
}
