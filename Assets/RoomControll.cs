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
        GameRoom.instance.Room.GameData.SetMapContent(NewChangeX, NewChangeY, PlayerBelongTag);
        GetComponent<MapView>().DrawMap(NewChangeX, NewChangeY, PlayerBelongTag);
    }

    public void OnPlayerHitBox(int x, int y)
    {

        if (GameRoom.instance.Room.GameData.GetMapContent(x, y) != 0)
        {
            return;
        }
        NewChangeX = x;
        NewChangeY = y;
        OnPlayerHitBox();
        ChangeTurn();
        GameRoom.instance.Push();

        GameEndRule.instance.Check();
        blocker.SetActive(true);

    }

    void ResetMap()
    {
        GameRoom.instance.Room = new Room();
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
