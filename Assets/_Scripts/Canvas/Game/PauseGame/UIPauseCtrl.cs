using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseCtrl : _MonoBehaviour
{
    private static UIPauseCtrl instance;
    public static UIPauseCtrl Instance { get => instance; }

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        if (UIPauseCtrl.instance != null) return;
        UIPauseCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCanvasGroup();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void SetAlphaCanvas(int alpha)
    {
        this.canvasGroup.alpha = alpha;
        this.canvasGroup.interactable = Convert.ToBoolean(alpha);
        this.canvasGroup.blocksRaycasts = Convert.ToBoolean(alpha);

    }
}
