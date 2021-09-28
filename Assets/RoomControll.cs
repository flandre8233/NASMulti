using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControll : SingletonMonoBehavior<RoomControll>
{
    public int PlayerBelongTag;
    int NewChangeX;
    int NewChangeY;

    public GameObject blocker;
    private void Start()
    {
        if (PlayerData.instance.Data.UID == GameRoom.instance.Room.Info.Player1)
        {
            PlayerBelongTag = 1;
        }
        if (PlayerData.instance.Data.UID == GameRoom.instance.Room.Info.Player2)
        {
            PlayerBelongTag = 2;
        }
        blocker.SetActive(!IsTurn());
    }

    void OnPlayerHitBox()
    {
        int x = NewChangeX;
        int y = NewChangeY;
        GetComponent<MapView>().DrawMap(x, y, PlayerBelongTag);
    }

    public void OnPlayerHitBox(int x, int y)
    {
        NewChangeX = x;
        NewChangeY = y;
        if (GameRoom.instance.Room.GameData.GetMapContent(NewChangeX, NewChangeY) != 0)
        {
            return;
        }
        GameRoom.instance.Room.GameData.SetMapContent(NewChangeX, NewChangeY, PlayerBelongTag);
        GetComponent<MapView>().DrawMap(x, y, PlayerBelongTag);

        GameRoom.instance.OnPulled += OnPlayerHitBox;
        GameRoom.instance.OnPulled += ChangeTurn;
        GameRoom.instance.Sync();

        GameEndRule.instance.Check();
        blocker.SetActive(true);

    }



    void ResetMap()
    {
        GameRoom.instance.Room = new Room();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LeaveCurrentRoom.Create();
        }
    }

    void ChangeTurn()
    {
        GameRoom.instance.Room.GameData.WhoTurn = (GameRoom.instance.Room.GameData.WhoTurn == 0) ? 1 : 0;
    }

    public bool IsTurn()
    {
        return (GameRoom.instance.Room.GameData.WhoTurn == (PlayerBelongTag - 1));
    }
}
