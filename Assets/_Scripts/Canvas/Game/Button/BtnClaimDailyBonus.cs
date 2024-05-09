using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BtnClaimDailyBonus : BaseButton {
  public PlayableDirector timeline;
    protected override void OnClick()
    {
        timeline.Play();
    }
}
