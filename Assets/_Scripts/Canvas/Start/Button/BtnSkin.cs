using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnSkin : BaseButton
{
    protected override void OnClick()
    {
        UICharacter.Instance.Toggle();
    }

}
