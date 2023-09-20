using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuit : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
