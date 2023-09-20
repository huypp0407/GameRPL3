using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRewradCtrl : _MonoBehaviour
{
    private static UIRewradCtrl instance;
    public static UIRewradCtrl Instance { get => instance; }

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    [SerializeField] protected AutoScroll autoScroll;
    public AutoScroll AutoScroll => autoScroll;

    [SerializeField] protected ScrollContent scrollContent;
    public ScrollContent ScrollContent => scrollContent;

    [SerializeField] protected RewardSpawner rewardSpawner;
    public RewardSpawner RewardSpawner => rewardSpawner;

    [SerializeField] protected Transform content;
    public Transform Content => content;

    protected override void Awake()
    {
        base.Awake();
        if (UIRewradCtrl.instance != null) return;
        UIRewradCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCanvasGroup();
        this.LoadAutoScroll();
        this.LoadScrollContent();
        this.LoadContent();
        this.LoadInvItemSpawner();
    }

    protected virtual void LoadContent()
    {
        if (this.content != null) return;
        this.content = transform.Find("Reward");
    }

    protected virtual void LoadScrollContent()
    {
        if (this.scrollContent != null) return;
        this.scrollContent = GetComponentInChildren<ScrollContent>();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    protected virtual void LoadInvItemSpawner()
    {
        if (this.rewardSpawner != null) return;
        this.rewardSpawner = GetComponentInChildren<RewardSpawner>();
    }

    protected virtual void LoadAutoScroll()
    {
        if (this.autoScroll != null) return;
        this.autoScroll = GetComponentInChildren<AutoScroll>();
    }

    public virtual void SetAlphaCanvas(int alpha)
    {
        this.canvasGroup.alpha = alpha;
        this.canvasGroup.interactable = Convert.ToBoolean(alpha);
        this.canvasGroup.blocksRaycasts = Convert.ToBoolean(alpha);

    }
}
