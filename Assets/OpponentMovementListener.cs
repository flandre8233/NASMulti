using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMovementListener : MonoBehaviour
{
    private void OnEnable()
    {
        InvokeRepeating("SyncMap", 2f, 5f);
    }

    void SyncMap()
    {
        GameRoom.instance.OnPulled += CheckMyTurnNow;
        GameRoom.instance.Sync();
    }

    void CheckMyTurnNow()
    {
        if (RoomControll.instance.IsTurn())
        {
            MapView.instance.DrawMap();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
