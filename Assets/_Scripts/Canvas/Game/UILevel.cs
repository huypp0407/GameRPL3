using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevel : _MonoBehaviour
{
    private static UILevel instance;
    public static UILevel Instance => instance;

    [SerializeField] protected UILevelCtrl levelCtrl;

    [SerializeField] protected bool isOpen = true;
    public bool IsOpen => isOpen;

    protected override void Awake()
    {
        base.Awake();
        if (UILevel.instance != null) return;
        UILevel.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.levelCtrl != null) return;
        this.levelCtrl = transform.parent.GetComponent<UILevelCtrl>();
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
        this.levelCtrl.SetValueSlider();
        this.levelCtrl.SetAlphaCanvas(1);
    }

    public virtual void Close()
    {
        this.levelCtrl.SetAlphaCanvas(0);
    }
}
