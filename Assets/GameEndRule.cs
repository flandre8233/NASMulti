using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndRule : SingletonMonoBehavior<GameEndRule>
{
    [SerializeField] GameObject blocker;
    public int EndReason = 0;
    public void Check()
    {
        if (GameRoom.instance.Room.GameData.IsFullPlayed())
        {
            Draw();
            return;
        }
        int WhoWin = GameRoom.instance.Room.GameData.CheckWhoConnectLine();
        if (WhoWin > 0)
        {
            if (WhoWin == RoomControll.instance.PlayerBelongTag)
            {
                PlayerWin();

            }
            else
            {
                PlayerLose();
            }
        }

    }

    void PlayerWin()
    {
        EndReason = 1;
        PlayerData.instance.OnPulled += WinCount;
        PlayerData.instance.Sync();
        LeaveCurrentRoom.Create();
        blocker.SetActive(true);
        RoomControll.instance.blocker.SetActive(false);
    }
    void PlayerLose()
    {
        EndReason = 2;
        PlayerData.instance.OnPulled += LoseCount;
        PlayerData.instance.Sync();
        LeaveCurrentRoom.Create();
        blocker.SetActive(true);
        RoomControll.instance.blocker.SetActive(false);

    }
    void Draw()
    {
        EndReason = 3;
        PlayerData.instance.OnPulled += LoseCount;
        PlayerData.instance.Sync();
        LeaveCurrentRoom.Create();
        blocker.SetActive(true);
        RoomControll.instance.blocker.SetActive(false);
    }

    void WinCount()
    {
        PlayerData.instance.Data.Round++;
        PlayerData.instance.Data.Win++;

    }
    void LoseCount()
    {
        PlayerData.instance.Data.Round++;

    }
    private void OnDisable()
    {
        blocker.SetActive(false);
    }
}
