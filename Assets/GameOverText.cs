using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverText : CommonUIText
{
    private void OnEnable()
    {
        UpdateText();
    }

    public override void UpdateText()
    {

        switch (GameEndRule.instance.EndReason)
        {
            case 1:
                text.text = "You Win";
                break;

            case 2:
                text.text = "You Lose";
                break;


            default:
                text.text = "Draw";
                break;
        }
    }
}
