using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public int x;
    public int y;
    private void OnMouseUp()
    {

        
        if (RoomControll.instance.IsTurn())
        {
            RoomControll.instance.OnPlayerHitBox(x, y);
        }
    }
}
