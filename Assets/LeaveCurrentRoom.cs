using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveCurrentRoom : MonoBehaviour
{
    public static void Create()
    {
        GameObject SpawnObject = new GameObject(typeof(LeaveCurrentRoom).ToString());
        LeaveCurrentRoom Component = SpawnObject.AddComponent<LeaveCurrentRoom>();
    }
    string LeavingRoomName;
    public void LeaveThisRoom()
    {
        LeavingRoomName = GameRoom.instance.Room.Info.RoomName;

        PlayerData.instance.OnPulled += DeLink;
        PlayerData.instance.Sync();

        GameRoom.instance.OnPulled += MarkLeaveID;
        GameRoom.instance.OnPulled += AbandonedCheck;
        GameRoom.instance.Sync();


    }

    void DeLink()
    {
        PlayerData.instance.Data.LinkingGame = "";
    }
    void MarkLeaveID()
    {
        GameRoom.instance.Room.LeaveID.Add(PlayerData.instance.Data.UID);
    }

    void AbandonedCheck()
    {
        if (GameRoom.instance.Room.IsAbandoned())
        {
            Rooms.instance.OnPulled += RemoveRoom;
            Rooms.instance.Sync();
            GameRoom.instance.Delete();
        }
    }

    void RemoveRoom()
    {
        List<RoomInfo> collection = Rooms.instance.RoomsInfo.infos;

        for (int i = 0; i < collection.Count; i++)
        {
            RoomInfo item = collection[i];
            if (item.RoomName == LeavingRoomName)
            {
                Rooms.instance.RoomsInfo.infos.Remove(item);
            }
        }
    }

}
