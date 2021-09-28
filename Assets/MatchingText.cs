using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingText : CommonUIText
{
    private void OnEnable()
    {
        UpdateText();
    }

    public override void UpdateText()
    {
        if (PlayerData.instance.Data.isNotLinking())
        {
            text.text = "Start Matching";
        }
        else
        {
            text.text = "Continue Game";
        }
    }
}
