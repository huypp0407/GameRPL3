using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScore : _MonoBehaviour
{
    private static TextScore instance;
    public static TextScore Instance => instance;

    [SerializeField] protected int score = 13;
    public int Score => score;

    [SerializeField] protected int kill = 0;
    public int Kill => kill;

    [SerializeField] protected Text textScore;

    public bool canUpgradeScore = true;

    protected override void Awake()
    {
        base.Awake();
        if (TextScore.instance != null) return;
        TextScore.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTextScore();
    }

    protected virtual void LoadTextScore()
    {
        if (this.textScore != null) return;
        this.textScore = GetComponent<Text>();
    }

    public virtual void UpdateScore()
    {
        this.kill++;
        if (!canUpgradeScore) return;
        this.score++;
        this.textScore.text = score.ToString();
    }

    public virtual void SetScore(int score)
    {
        this.score = score;
        this.textScore.text = score.ToString();
    }

    //public int avgFrameRate;
    //private void Update()
    //{
    //    float current = 0;
    //    current = Time.frameCount / Time.time;
    //    avgFrameRate = (int)current;
    //    this.textScore.text = avgFrameRate.ToString() + " FPS";
    //}
}
