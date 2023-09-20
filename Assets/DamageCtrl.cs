using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class DamageCtrl : _MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 0.3f;

    public Vector3 direction = Vector3.up;

    [SerializeField] protected TextMeshPro textMeshPro;

    [SerializeField] protected float disappearTimer;
    [SerializeField] protected Color color;

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.textMeshPro != null) return;
        this.textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    public void SetUp(bool isCrit, float damage)
    {
        textMeshPro.SetText(Math.Round(damage,2).ToString());
        if (isCrit)
        {
            textMeshPro.fontSize = 2;
            color = Color.red;
        }
        else
        {
            textMeshPro.fontSize = 1.5f;
            color = Color.yellow;
        }
        textMeshPro.color = color;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        transform.localScale = new Vector3(.5f, .5f, .5f);
        transform.DOScale(new Vector3(1f, 1f, 1f), .15f);
        //transform.localScale -= Vector3.one * Time.deltaTime;
    }

    private void Update()
    {
        transform.position += this.direction * Time.deltaTime * 4f;
        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            this.color.a -= 4f * Time.deltaTime;
            textMeshPro.color = color;
            if(color.a < 0)
            {
                FXSpawner.Instance.Despawn(transform);
            }
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + GameCtrl.Instance.MainCamera.transform.forward);
    }
}
