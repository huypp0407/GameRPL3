using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelCtrl : _MonoBehaviour
{
    private static UILevelCtrl instance;
    public static UILevelCtrl Instance { get => instance; }

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    [SerializeField] protected CanvasGroup canvasGroupLevel;
    public CanvasGroup CanvasGroupLevel => canvasGroupLevel;

    [SerializeField] protected Slider slider;
    public Slider Slider => slider;

    [SerializeField] protected Text levelCurrent;
    [SerializeField] protected Text levelNext;
    [SerializeField] protected Text kill;
    [SerializeField] protected Text time;
    [SerializeField] protected float timer = 0f;

    protected float minValue = 0;

    protected override void Awake()
    {
        base.Awake();
        if (UILevelCtrl.instance != null) return;
        UILevelCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCanvasGroup();
        this.LoadSlider();
        this.LoadSliderNext();
        this.LoadLevelCurrent();
        this.LoadLevelNext();
    }

    protected virtual void LoadLevelCurrent()
    {
        if (this.levelCurrent != null) return;
        this.levelCurrent = GameObject.Find("CurrentLevel").GetComponentInChildren<Text>();
    }

    protected virtual void LoadLevelNext()
    {
        if (this.levelNext != null) return;
        this.levelNext = GameObject.Find("NextLevel").GetComponentInChildren<Text>();
    }

    protected virtual void LoadSliderNext()
    {
        if (this.canvasGroupLevel != null) return;
        this.canvasGroupLevel = GameObject.Find("NextLevel").GetComponent<CanvasGroup>();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void LoadSlider()
    {
        if (this.slider != null) return;
        this.slider = GetComponentInChildren<Slider>();
    }

    public virtual void SetAlphaCanvas(int alpha)
    {
        this.canvasGroup.alpha = alpha;
        this.canvasGroup.interactable = Convert.ToBoolean(alpha);
        this.canvasGroup.blocksRaycasts = Convert.ToBoolean(alpha);
    }

    private void Update()
    {
        this.timer += Time.deltaTime;
    }

    public virtual void SetValueSlider()
    {
        Debug.Log(TextScore.Instance.Kill);
        Debug.Log(TimeSpan.FromSeconds(this.timer).ToString("hh':'mm':'ss"));
        this.kill.text = TextScore.Instance.Kill.ToString();
        this.time.text = TimeSpan.FromSeconds(this.timer).ToString("hh':'mm':'ss");
        this.levelCurrent.text = MapLevel.Instance.LevelCurrent.ToString();
        this.levelNext.text = (MapLevel.Instance.LevelCurrent + 1).ToString();
        StartCoroutine(SliderFill());
    }

    IEnumerator SliderFill()
    {
        while (this.slider.value < 1)
        {
            this.slider.value += 0.005f;
            yield return new WaitForSeconds(0.005f);
        }
        this.canvasGroupLevel.alpha = 1f;
        StateGameCtrl.nextLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
