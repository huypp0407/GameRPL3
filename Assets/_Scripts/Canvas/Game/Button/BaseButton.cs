using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : _MonoBehaviour
{
    [SerializeField] protected Button button;

    protected override void Start()
    {
        base.Start();
        this.AddButtonOnClick();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadButton();
    }

    protected virtual void LoadButton()
    {
        if (button != null) return;
        this.button = transform.GetComponent<Button>();
    }

    protected virtual void AddButtonOnClick()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();
}