using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BaseCoolDown : _MonoBehaviour
{
    [SerializeField] protected float timer;
    [SerializeField] protected float delay;
    [SerializeField] protected string coolDown;

    [SerializeField] protected Image image;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTextScore();
    }

    protected virtual void LoadTextScore()
    {
        if (this.image != null) return;
        this.image = GetComponent<Image>();
    }

    public virtual void Update()
    {
        this.image.fillAmount = (delay - timer) / delay;
    }
}
