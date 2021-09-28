using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitButton : CommonUIButton
{
    protected override void Click()
    {
        StatusChanger.instance.ChangeToLobby();
        LeaveCurrentRoom.instance.LeaveThisRoom();
    }
}
