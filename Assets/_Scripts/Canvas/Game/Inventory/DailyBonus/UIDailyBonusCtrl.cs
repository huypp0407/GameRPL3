using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIDailyBonusCtrl : _MonoBehaviour
{
    private static UIDailyBonusCtrl instance;
    public static UIDailyBonusCtrl Instance { get => instance; }

    [SerializeField] protected UIDailyBonus dailyBonus;
    public UIDailyBonus DailyBonus => dailyBonus;

    [SerializeField] protected DailyBonusSpawner dailyBonusSpawner;
    public DailyBonusSpawner DailyBonusSpawner => DailyBonusSpawner;

    [SerializeField] protected Transform content;
    public Transform Content => content;

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        if (UIDailyBonusCtrl.instance != null) return;
        UIDailyBonusCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadDailyBonusSpawner();
        this.LoadContent();
        this.LoadUIDailyBonus();
        this.LoadCanvasGroup();
    }

    protected virtual void LoadUIDailyBonus()
    {
        if (this.dailyBonus != null) return;
        this.dailyBonus = GetComponentInChildren<UIDailyBonus>();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void LoadDailyBonusSpawner()
    {
        if (this.dailyBonusSpawner != null) return;
        this.dailyBonusSpawner = GetComponentInChildren<DailyBonusSpawner>();
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
