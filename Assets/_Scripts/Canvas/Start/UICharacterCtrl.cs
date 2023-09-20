using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterCtrl : _MonoBehaviour
{
    private static UICharacterCtrl instance;
    public static UICharacterCtrl Instance { get => instance; }

    [SerializeField] protected UICharacter character;
    public UICharacter Character => character;

    [SerializeField] protected CharacterSpawner characterSpawner;
    public CharacterSpawner CharacterSpawner => characterSpawner;

    [SerializeField] protected Transform content;
    public Transform Content => content;

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    protected override void Awake()
    {
        base.Awake();
        if (UICharacterCtrl.instance != null) return;
        UICharacterCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCharacterSpawner();
        this.LoadContent();
        this.LoadUICharacter();
        this.LoadCanvasGroup();
    }

    protected virtual void LoadUICharacter()
    {
        if (this.character != null) return;
        this.character = GetComponentInChildren<UICharacter>();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void LoadCharacterSpawner()
    {
        if (this.characterSpawner != null) return;
        this.characterSpawner = GetComponentInChildren<CharacterSpawner>();
    }

    protected virtual void LoadContent()
    {
        if (this.content != null) return;
        this.content = transform.Find("Scroll View").Find("Viewport").Find("Content");
    }

    public virtual void SetAlphaCanvas(int alpha)
    {
        this.canvasGroup.alpha = alpha;
        this.canvasGroup.interactable = Convert.ToBoolean(alpha);
        this.canvasGroup.blocksRaycasts = Convert.ToBoolean(alpha);
    }
}