using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : _MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSlider();
        this.LoadImageFill();
    }

    protected virtual void LoadSlider()
    {
        if (this.slider != null) return;
        this.slider = GetComponent<Slider>();
    }

    protected virtual void LoadImageFill()
    {
        if (this.fill != null) return;
        this.fill = GetComponentInChildren<Image>();
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
