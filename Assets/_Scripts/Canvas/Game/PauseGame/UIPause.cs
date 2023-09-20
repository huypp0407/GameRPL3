using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPause : _MonoBehaviour
{
    private static UIPause instance;
    public static UIPause Instance => instance;

    [SerializeField] protected UIPauseCtrl pauseCtrl;

    [SerializeField] protected bool isOpen = true;
    public bool IsOpen => isOpen;

    protected override void Awake()
    {
        base.Awake();
        if (UIPause.instance != null) return;
        UIPause.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.pauseCtrl != null) return;
        this.pauseCtrl = transform.parent.GetComponent<UIPauseCtrl>();
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
        this.pauseCtrl.SetAlphaCanvas(1);
        Time.timeScale = 0;

    }

    public virtual void Close()
    {
        this.pauseCtrl.SetAlphaCanvas(0);
        Time.timeScale = 1;

    }
}
