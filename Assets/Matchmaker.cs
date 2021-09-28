using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using GibSYNO;
using System.Linq;
public class Matchmaker : MonoBehaviour
{

    public string MatchGameID;
    RoomInfo TargetInfo;
    public static void Create()
    {
        GameObject SpawnObject = new GameObject(typeof(Matchmaker).ToString());
        Matchmaker Component = SpawnObject.AddComponent<Matchmaker>();
    }

    private void Start()
    {
        Rooms.instance.OnPulled += InfoUpdated;
        Rooms.instance.OnPushed += OnComplete;
        Rooms.instance.Sync();
    }

    void InfoUpdated()
    {
        TargetInfo = Rooms.instance.RoomsInfo.GetEmptyRoom();
        if (TargetInfo != null)
        {
            //join room
            print("JoinRoom");
            TargetInfo.Join(PlayerData.instance.Data.UID);
        }
        else
        {
            //open room
            print("OpenRoom");
            TargetInfo = new RoomInfo(PlayerData.instance.Data.UID);
            Rooms.instance.RoomsInfo.infos.Add(TargetInfo);
        }
        MatchGameID = TargetInfo.RoomName;

        GameRoom.instance.OnPulled += SyncRoomInfo;
        GameRoom.instance.OnPulled += GameRoom.instance.UpdateCommitVersion;
        GameRoom.instance.Sync();
    }

    void SyncRoomInfo()
    {
        GameRoom.instance.Room.Info = TargetInfo;
    }

    void OnComplete()
    {
        PlayerData.instance.Data.LinkingGame = MatchGameID;
        PlayerData.instance.Push();
        Destroy(gameObject);

        GameRoomCreator.Create();
    }

}
