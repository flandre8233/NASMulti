using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWLText : CommonUIText
{
    private void OnEnable()
    {
        UpdateText();
    }

    public override void UpdateText()
    {
        text.text = "Win : " + PlayerData.instance.Data.Win;
        text.text += "  Lose : " + (PlayerData.instance.Data.Round - PlayerData.instance.Data.Win);
    }
}
