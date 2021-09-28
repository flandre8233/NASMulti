using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using GibSYNO;
using System.Linq;
public delegate void DG();
public class Rooms : SingletonMonoBehavior<Rooms>
{
    public RoomsInfo RoomsInfo;
    public DG OnPulled;
    public DG OnPushed;

    private void Start()
    {
        RoomsInfo = new RoomsInfo();
    }
    public void Pull()
    {
        IRequest request = new Download("RoomsInfo", "/Rooms");
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
        RoomsInfo = JsonMapper.ToObject<RoomsInfo>(Result);
        Pulled();
    }

    public void Push()
    {
        IRequest request = new Upload("RoomsInfo", "/Rooms", ToJsonString(RoomsInfo));
        request.AddOnResponsed(Pushed);
        request.Request();
    }

    public void Sync()
    {
        OnPulled += Push;
        Pull();
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


    string ToJsonString(object obj)
    {
        JsonWriter writer = new JsonWriter();
        JsonMapper.ToJson(obj, writer);
        return writer.ToString();
    }



}

[System.Serializable]
public class RoomsInfo
{
    public int UID;
    public List<RoomInfo> infos;

    public RoomsInfo()
    {
        infos = new List<RoomInfo>();
    }

    public RoomInfo GetEmptyRoom()
    {
        return infos.Where(x => string.IsNullOrEmpty(x.Player2)).FirstOrDefault();
    }

    public int GetUID()
    {
        int Output = UID;
        UID++;
        return Output;
    }


}

[System.Serializable]
public class RoomInfo
{
    public string RoomName;
    public string Player1;
    public string Player2;

    public RoomInfo()
    {
    }

    public RoomInfo(string Host)
    {
        RoomName = "Room_" + Rooms.instance.RoomsInfo.GetUID();
        Player1 = Host;
    }

    public void Join(string Client)
    {
        Player2 = Client;
    }

}
