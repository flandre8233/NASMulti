using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusControll : SingletonMonoBehavior<StatusControll>
{
    string CurrentStatus;
    GameObject[] StatusObjects;
    Dictionary<string, int> IndexDict;

    private void Start()
    {
        StatusObjects = GetAllChild();
        BindingStatusObject();
        ToNewStatus("Login");
    }

    GameObject[] GetAllChild()
    {
        List<GameObject> AllChild = new List<GameObject>();
        foreach (Transform item in transform)
        {
            AllChild.Add(item.gameObject);
        }
        return AllChild.ToArray();
    }

    void BindingStatusObject()
    {
        IndexDict = new Dictionary<string, int>();
        for (int i = 0; i < StatusObjects.Length; i++)
        {
            GameObject item = StatusObjects[i];
            IndexDict.Add(item.name, i);
            print(item.name + ":" + i);
        }
    }

    public void ToNewStatus(string NewStatus)
    {
        CurrentStatus = NewStatus;
        UpdateStatusObject();
    }

    void UpdateStatusObject()
    {
        int WantedIndex = IndexDict[CurrentStatus];
        print("WantedIndex" + ":" + WantedIndex);

        for (int i = 0; i < StatusObjects.Length; i++)
        {
            StatusObjects[i].SetActive(i == WantedIndex);
        }
    }

}
