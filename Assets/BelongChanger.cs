using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelongChanger : MonoBehaviour
{
    public void Belong(int belong)
    {
        GetComponent<MeshRenderer>().material.color = GetColor(belong);
    }

    Color GetColor(int belong)
    {
        switch (belong)
        {
            case 1:
                return Color.red;
            case 2:
                return Color.blue;
            default:
                return Color.white;
        }
    }
}
