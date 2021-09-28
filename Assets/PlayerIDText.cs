using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDText : CommonUIText
{
    private void OnEnable()
    {
        UpdateText();
    }

    public override void UpdateText()
    {
        text.text = "WelcomeBack!! : " + PlayerData.instance.Data.UID;
    }
}
