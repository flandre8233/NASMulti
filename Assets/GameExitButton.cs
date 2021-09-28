using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitButton : CommonUIButton
{
    protected override void Click()
    {
        GameRoom.instance.Room = new Room();
        StatusChanger.instance.ChangeToLobby();
    }
}
