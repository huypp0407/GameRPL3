using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnNewGame : BaseButton
{
    protected override void OnClick()
    {
      StateGameCtrl.isNewGame = true;
      AsyncLoader.Instance.LoadLevel("start");
    }

}
