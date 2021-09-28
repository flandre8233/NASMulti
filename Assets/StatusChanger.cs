using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusChanger : SingletonMonoBehavior<StatusChanger>
{
    public void ChangeToLogin()
    {
        StatusControll.instance.ToNewStatus("Login");
    }
    public void ChangeToLobby()
    {
        StatusControll.instance.ToNewStatus("Lobby");
    }
    public void ChangeToGame()
    {
        StatusControll.instance.ToNewStatus("Game");
    }
}
