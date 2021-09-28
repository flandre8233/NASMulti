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
        GameRoom.instance.OnPushed += CheckMyTurnNow;
        GameRoom.instance.Sync();
    }

    void CheckMyTurnNow()
    {
        if (RoomControll.instance.IsTurn())
        {
            MapView.instance.DrawMap();
            gameObject.SetActive(false);
        }
        GameEndRule.instance.Check();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
