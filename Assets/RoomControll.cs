using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControll : SingletonMonoBehavior<RoomControll>
{
    int PlayerBelongTag;
    int NewChangeX;
    int NewChangeY;

    [SerializeField]
    GameObject blocker;
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
        GameRoom.instance.Room.GameData.SetMapContent(x, y, PlayerBelongTag);
        GetComponent<MapView>().DrawMap(x, y);
    }

    public void OnPlayerHitBox(int x, int y)
    {
        if (GameRoom.instance.Room.GameData.GetMapContent(x, y) == PlayerBelongTag)
        {
            return;
        }

        GetComponent<MapView>().DrawMapUnSync(x, y, PlayerBelongTag);
        blocker.SetActive(true);

        NewChangeX = x;
        NewChangeY = y;
        GameRoom.instance.OnPulled += OnPlayerHitBox;
        GameRoom.instance.OnPulled += ChangeTurn;
        GameRoom.instance.Sync();
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
