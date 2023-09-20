using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnContinue : BaseButton
{
    protected override void OnClick()
    {
        MainMenu.Instance.Continue();
    }

}
