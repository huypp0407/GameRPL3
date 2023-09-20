using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class VolumePost : _MonoBehaviour
{
    private static VolumePost instance;
    public static VolumePost Instance => instance;

    public PostProcessVolume volume;
    public Vignette vignette;

    protected override void Awake()
    {
        base.Awake();
        if (VolumePost.instance != null) return;
        VolumePost.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPostProcessVolume();
    }

    protected virtual void LoadPostProcessVolume()
    {
        if (this.volume != null) return;

        this.volume = GetComponent<PostProcessVolume>();
    }

    protected override void Start()
    {
        this.volume.profile.TryGetSettings(out vignette);
        this.vignette.intensity.value = 0f;
    }

    IEnumerator Flicker()
    {
        float t = 0.25f;
        while (true)
        {
            t = Mathf.Abs(Mathf.Abs(t - 0.25f) - 0.4f);
            DOTween.To(() => this.vignette.intensity.value, x => this.vignette.intensity.value = x, t, .5f);
            yield return new WaitForSeconds(1f);
        }
    }

    public virtual void StartFlicker()
    {
        StartCoroutine(Flicker());
    }

    public virtual void StopFlicker()
    {
        DOTween.To(() => this.vignette.intensity.value, x => this.vignette.intensity.value = x, 0, 1.5f);
        StopAllCoroutines();
    }
}
