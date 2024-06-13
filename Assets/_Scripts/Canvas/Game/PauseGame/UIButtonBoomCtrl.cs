using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonBoomCtrl : _MonoBehaviour
{
    private static UIButtonBoomCtrl instance;
    public static UIButtonBoomCtrl Instance { get => instance; }

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    [SerializeField] protected BtnThrowBomb _btnThrowBomb;
    protected override void Awake()
    {
        base.Awake();
        if (UIButtonBoomCtrl.instance != null) return;
        UIButtonBoomCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCanvasGroup();
        this.LoadBtnThrowBom();
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
    
    public virtual void LoadBtnThrowBom()
    {
      if (this._btnThrowBomb != null) return;
      this._btnThrowBomb = GetComponentInChildren<BtnThrowBomb>();
    }

    public void SetTypeBomb(string typeBomb) {
      this._btnThrowBomb.typebomb = typeBomb;
    }
}
