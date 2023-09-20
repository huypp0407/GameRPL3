using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BtnReward : BaseButton
{
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 20f;
    [SerializeField] protected bool isReady = true;
    [SerializeField] protected Image image;
    //protected virtual void FixedUpdate()
    //{
    //    this.Timing();
    //}

    //protected virtual void Timing()
    //{
    //    if (this.isReady) return;
    //    this.timer += Time.fixedDeltaTime;
    //    if (this.timer < this.delay)
    //    {
    //        this.image.fillAmount = (delay - timer) / delay;
    //        return;
    //    }

    //    this.isReady = true;
    //}

    //public virtual void Active()
    //{
    //}

    protected override void OnClick()
    {
        if (!isReady) return;
        AdmobAds.Instance.ShowRewardAd();
        this.isReady = false;
        StartCoroutine(Timing());
    }

    IEnumerator Timing()
    {
        //this.image.fillAmount = 1;
        Tween tween =  DOTween.To(x => this.image.fillAmount = x, 1, 0, 20f).SetUpdate(true);
        yield return tween.WaitForCompletion();
        this.isReady = true;

    }
}
