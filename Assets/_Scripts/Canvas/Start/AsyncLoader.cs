using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : _MonoBehaviour
{
    [SerializeField] protected static AsyncLoader instance;
    public static AsyncLoader Instance => instance;

    [SerializeField] protected GameObject loadingSceen;

    [SerializeField] protected Slider loadingSlider;

    protected override void Awake()
    {
        base.Awake();
        if (AsyncLoader.instance != null) return;
        AsyncLoader.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadScene();
        this.LoadLoadingSlider();
    }

    protected virtual void LoadScene()
    {
        if (this.loadingSceen != null) return;
        this.loadingSceen = GameObject.Find("LoadScene");
        this.loadingSceen.SetActive(false);
    }

    protected virtual void LoadLoadingSlider()
    {
        if (this.loadingSlider != null) return;
        this.loadingSlider = this.loadingSceen.GetComponentInChildren<Slider>();
    }

    public void LoadLevel(string levelToRoad)
    {
        loadingSceen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToRoad));
    }

    IEnumerator LoadLevelAsync(string levelToRoad)
    {
        // yield return new WaitForSeconds(5f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelToRoad);

        while (!asyncOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
