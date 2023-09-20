using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIInventoryCtrl : _MonoBehaviour
{
    private static UIInventoryCtrl instance;
    public static UIInventoryCtrl Instance { get => instance; }

    [SerializeField] protected UIInventory inventory;
    public UIInventory UIInventory => inventory;

    [SerializeField] protected InvItemSpawner invItemSpawner;
    public InvItemSpawner InvItemSpawner => invItemSpawner;

    [SerializeField] protected Transform content;
    public Transform Content => content;

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        if (UIInventoryCtrl.instance != null) return;
        UIInventoryCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadInvItemSpawner();
        this.LoadContent();
        this.LoadUIInventory();
        this.LoadCanvasGroup();
    }

    protected virtual void LoadUIInventory()
    {
        if (this.inventory != null) return;
        this.inventory = GetComponentInChildren<UIInventory>();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void LoadInvItemSpawner()
    {
        if (this.invItemSpawner != null) return;
        this.invItemSpawner = GetComponentInChildren<InvItemSpawner>();
    }

    protected virtual void LoadContent()
    {
        if (this.content != null) return;
        this.content = transform.Find("Scroll View").Find("Viewport").Find("Content");
    }

    public virtual void SetAlphaCanvas(float alpha, float time)
    {
        this.canvasGroup.DOFade(alpha, time).SetUpdate(true);
        this.canvasGroup.interactable = Convert.ToBoolean(alpha);
        this.canvasGroup.blocksRaycasts = Convert.ToBoolean(alpha);
    }
}
