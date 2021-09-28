using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingButton : CommonUIButton
{
    private void OnEnable()
    {
        button.interactable = true;
    }

    protected override void Click()
    {
        if (PlayerData.instance.Data.isNotLinking())
        {
            Matchmaker.Create();
        }
        else
        {
            GameRoomCreator.Create();
        }

        button.interactable = false;
    }
}
