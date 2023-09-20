using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseCharacter : BaseButton
{
    protected override void OnClick()
    {
        UICharacter.Instance.Toggle();
    }

}
