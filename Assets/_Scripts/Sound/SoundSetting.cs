using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSetting : _MonoBehaviour
{
    [SerializeField] protected AudioMixer audioMixer;
    [SerializeField] protected Slider allSlider;
    [SerializeField] protected Slider musicSlider;
    [SerializeField] protected Slider effectSlider;

    protected override void Start()
    {
        base.Start();
        if (PlayerPrefs.HasKey("AllVolume"))
        {
            this.LoadVolume();
        }
        else
        {
            SetAllVolume();
            SetEffectVolume();
            SetMusicVolume();
        }
    }

    public void SetAllVolume()
    {
        float volume = allSlider.value;
        audioMixer.SetFloat("Master",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("AllVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetEffectVolume()
    {
        float volume = effectSlider.value;
        audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("EffectVolume", volume);
    }

    public virtual void LoadVolume()
    {
        allSlider.value = PlayerPrefs.GetFloat("AllVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        SetAllVolume();
        SetMusicVolume();
        SetEffectVolume();
    }
}
