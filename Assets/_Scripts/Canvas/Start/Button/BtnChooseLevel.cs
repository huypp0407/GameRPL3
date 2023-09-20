using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnChooseLevel : BaseButton
{
    protected override void OnClick()
    {
        string level = transform.GetComponentInChildren<Text>().text;
        StateGameCtrl.level = int.Parse(level);
        StateGameCtrl.chooseLevel = true;
        Debug.Log(StateGameCtrl.chooseLevel);
    }
}
