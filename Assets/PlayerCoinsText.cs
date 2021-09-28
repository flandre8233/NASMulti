using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinsText : CommonUIText
{
    private void OnEnable()
    {
        UpdateText();
    }

    public override void UpdateText()
    {
        text.text = "Coins : " + PlayerData.instance.Data.Coins;
    }
}
