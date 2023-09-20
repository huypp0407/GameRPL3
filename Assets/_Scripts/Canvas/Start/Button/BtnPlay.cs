using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPlay : BaseButton
{
    protected override void OnClick()
    {
        MainMenu.Instance.PlayGame();
    }

}
