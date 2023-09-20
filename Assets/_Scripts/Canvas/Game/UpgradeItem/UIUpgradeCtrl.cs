using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIUpgradeCtrl : _MonoBehaviour
{
    private static UIUpgradeCtrl instance;
    public static UIUpgradeCtrl Instance { get => instance; }

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    [SerializeField] protected TextUpgradeCtrl textUpgradeCtrl;
    public TextUpgradeCtrl TextUpgradeCtrl => textUpgradeCtrl;

    [SerializeField] protected UpgradeSpawner upgradeSpawner;
    public UpgradeSpawner UpgradeSpawner => upgradeSpawner;

    protected override void Awake()
    {
        base.Awake();
        if (UIUpgradeCtrl.instance != null) return;
        UIUpgradeCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCanvasGroup();
        this.LoadTextUpgradeCtrl();
        this.LoadUpgradeSpawner();
    }

    protected virtual void LoadUpgradeSpawner()
    {
        if (this.upgradeSpawner != null) return;
        this.upgradeSpawner = GetComponentInChildren<UpgradeSpawner>();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void LoadTextUpgradeCtrl()
    {
        if (this.textUpgradeCtrl != null) return;
        this.textUpgradeCtrl = GetComponentInChildren<TextUpgradeCtrl>();
    }

    public virtual void SetAlphaCanvas(float alpha, float time)
    {
        this.canvasGroup.DOFade(alpha, time).SetUpdate(true);
        this.canvasGroup.interactable = Convert.ToBoolean(alpha);
        this.canvasGroup.blocksRaycasts = Convert.ToBoolean(alpha);
    }
}
