using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMovementListener : MonoBehaviour
{
    private void OnEnable()
    {
        InvokeRepeating("SyncMap", 5f, 10f);
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
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
