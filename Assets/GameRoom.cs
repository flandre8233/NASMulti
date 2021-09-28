using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using GibSYNO;
using System.Linq;
public class GameRoom : SingletonMonoBehavior<GameRoom>
{
    bool IsServerExistFile;
    public Room Room;
    public DG OnListed;
    public DG OnPulled;
    public DG OnPushed;
    private void Start()
    {
        Room = new Room();
    }

    public void Sync()
    {
        OnListed += PullIfExist;
        OnPulled += Push;
        List();
    }

    void List()
    {
        IRequest request = new List("/Rooms");
        request.AddOnResponsed(ListResponsed);
        request.Request();
    }

    void ListResponsed(string Result)
    {
        JsonData jsonData = JsonMapper.ToObject(Result);
        IsServerExistFile = IsExist(jsonData);
        Listed();
    }

    bool IsExist(JsonData jsonData)
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

    void PullIfExist()
    {
        if (IsServerExistFile)
        {
            Pull();
        }
        else
        {
            //Skip
            Pulled();
        }
    }

    public void Pull()
    {
        IRequest request = new Download(PlayerData.instance.Data.LinkingGame, "/Rooms");
        request.AddOnResponsed(PullResponsed);
        request.Request();
    }

    void PullResponsed(string Result)
    {
        if (string.IsNullOrEmpty(Result))
        {
            return;
        }
        print(Result);
        Room = JsonMapper.ToObject<Room>(Result);
        Pulled();
    }

    public void Push()
    {
        if (Room.Info == null)
        {
            return;
        }

        IRequest request = new Upload(Room.Info.RoomName, "/Rooms", ToJsonString(Room));
        request.AddOnResponsed(Pushed);
        request.Request();
    }

    public void Delete()
    {
        IRequest request = new Delete(Room.Info.RoomName, "/Rooms");
        request.Request();
        Room = new Room();
    }

    void Listed()
    {
        if (OnListed != null)
        {
            OnListed.Invoke();
            OnListed = null;
        }
    }

    void Pulled()
    {
        if (OnPulled != null)
        {
            OnPulled.Invoke();
            OnPulled = null;
        }
    }
    void Pushed()
    {
        if (OnPushed != null)
        {
            OnPushed.Invoke();
            OnPushed = null;
        }
    }

    void ResetEvents()
    {
        OnListed = null;
        OnPulled = null;
        OnPushed = null;
    }

    string ToJsonString(object obj)
    {
        JsonWriter writer = new JsonWriter();
        JsonMapper.ToJson(obj, writer);
        return writer.ToString();
    }
}

[System.Serializable]
public class Room
{
    public RoomInfo Info;

    public List<string> LeaveID;
    public GameData GameData;

    public Room()
    {
        LeaveID = new List<string>();
        GameData = new GameData();
    }
    public Room(RoomInfo _Info)
    {
        Info = _Info;
        LeaveID = new List<string>();
        GameData = new GameData();
    }

    public bool IsAbandoned()
    {
        return LeaveID.Count >= 2;
    }
}

public class GameData
{
    public int WhoTurn;

    public int[] Map;

    public GameData()
    {
        WhoTurn = 0;
        Map = new int[3 * 3];
    }

    public int GetMapContent(int x, int y)
    {
        return Map[(y * 3) + x];
    }
    public void SetMapContent(int x, int y, int Content)
    {
        Map[(y * 3) + x] = Content;
    }

    public bool IsFullPlayed()
    {
        foreach (var item in Map)
        {
            if (item <= 0)
            {
                return false;
            }
        }
        return true;
    }

    public int CheckWhoConnectLine()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Map[i] != 0 && Map[i] == Map[i + 3] && Map[i] == Map[i + 6])
            {
                return Map[i];
            }
            if (Map[i*3] != 0 && Map[i] == Map[i + 1] && Map[i] == Map[i + 2])
            {
                return Map[i];
            }
        }
        if (Map[4] != 0 && Map[0] == Map[4] && Map[0] == Map[8])
        {
            return Map[0];
        }

        if (Map[4] != 0 && Map[2] == Map[4] && Map[2] == Map[6])
        {
            return Map[0];
        }

        return 0;

    }



}
