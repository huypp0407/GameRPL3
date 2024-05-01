using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : _MonoBehaviour {
  [SerializeField] protected GameObject lockedImage;
  [SerializeField] protected GameObject[] stars;
  [SerializeField] protected Sprite starUnlock;
  private bool unlocked = false;

  protected override void Start() {
    PlayerPrefs.SetInt("Lv1", 1);
    PlayerPrefs.SetInt("Lv2", 2);
    PlayerPrefs.SetInt("Lv3", 3);
    UpdateLevelStatus();
    UpdateLevelStar();
  }

  public void UpdateLevelStatus() {
    int preLevel = int.Parse(gameObject.name) - 1;
    if(PlayerPrefs.GetInt("Lv" + preLevel) > 0 || gameObject.name == "1") {
      unlocked = true;
    }
  }

  public void UpdateLevelStar() {
    lockedImage.SetActive(!unlocked);
    for(int i = 0; i < stars.Length; i++)
    {
      stars[i].SetActive(unlocked);
    }

    if (unlocked) {
      int starNumber = PlayerPrefs.GetInt("Lv" + int.Parse(gameObject.name));
      for (int i = 0; i < starNumber; i++) {
        stars[i].gameObject.GetComponent<Image>().sprite = starUnlock;
      }
    }
  }

  public void onSelectLevel() {
    if(!unlocked) return;
    StateGameCtrl.level = int.Parse(gameObject.name);
    Debug.LogError($"HUYPP :: StateGameCtrl.level");
    AsyncLoader.Instance.LoadLevel("lv" + gameObject.name);
  }
}
