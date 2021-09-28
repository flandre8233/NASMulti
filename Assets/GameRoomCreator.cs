using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using GibSYNO;
using System.Linq;
public class GameRoomCreator : MonoBehaviour
{

    public static void Create()
    {
        GameObject SpawnObject = new GameObject(typeof(GameRoomCreator).ToString());
        GameRoomCreator Component = SpawnObject.AddComponent<GameRoomCreator>();
    }

    void Start()
    {
        List();
        //GameRoom.instance.Pull();
    }

    void List()
    {
        IRequest request = new List("/Rooms");
        request.AddOnResponsed(ListResult);
        request.Request();
    }
    void ListResult(string Result)
    {
        JsonData jsonData = JsonMapper.ToObject(Result);
        if (IsRoomOpened(jsonData))
        {
            //Read room
            GameRoom.instance.OnPushed += OnComplete;
            GameRoom.instance.Sync();
        }
        else
        {
            //create one
            GameRoom.instance.Room = new Room(new RoomInfo(PlayerData.instance.Data.UID));
            GameRoom.instance.OnPushed += OnComplete;
            GameRoom.instance.Push();
        }
    }

    bool IsRoomOpened(JsonData jsonData)
    {
        int total = int.Parse(jsonData["data"]["total"].ToString());
        for (int i = 0; i < total; i++)
        {
            if (jsonData["data"]["files"][i]["name"].ToString() == PlayerData.instance.Data.LinkingGame + ".txt")
            {
                return true;
            }
        }
        return false;
    }

    void OnComplete()
    {
        Destroy(gameObject);

        StatusChanger.instance.ChangeToGame();
    }

}
