using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class LevelSelection : _MonoBehaviour {
  [SerializeField] protected CanvasGroup lockedImage;
  private bool unlocked = false;
  [SerializeField] protected PlayableDirector timeline;

  protected override void Start() {
    // PlayerPrefs.SetInt("Lv1", 1);
    // PlayerPrefs.SetInt("Lv2", 0);
    // PlayerPrefs.SetInt("Lv3", 0);
    // PlayerPrefs.SetInt("Timelinee-Lv2", 0);
    // PlayerPrefs.SetInt("Timelinee-Lv3", 0);
    UpdateLevelStatus();
  }

  public void UpdateLevelStatus() {
    int preLevel = int.Parse(gameObject.name);
    if (PlayerPrefs.GetInt("Timelinee-Lv" + preLevel) > 0 && gameObject.name != "1") {
      timeline.Play();
      PlayerPrefs.SetInt("Timelinee-Lv" + preLevel, 0);
    }
    if(PlayerPrefs.GetInt("Lv" + preLevel) > 0 || gameObject.name == "1") {
      unlocked = true;
    }
    lockedImage.alpha = !unlocked ? 1 : 0;
  }

  public void onSelectLevel() {
    if(!unlocked) return;
    SaveManager.Instance.SaveGame();
    StateGameCtrl.level = int.Parse(gameObject.name);
    AsyncLoader.Instance.LoadLevel("lv" + gameObject.name);
  }
}
