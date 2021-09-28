using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GibSYNO;
using LitJson;
using System.IO;
using System;

public class PlayerData : SingletonMonoBehavior<PlayerData>
{
    public Data Data;
    public DG OnPulled;
    public DG OnPushed;

    void Pull()
    {
        IRequest Checker = new Download(Data.UID, "/Accouts");
        Checker.AddOnResponsed(PullResponsed);
        Checker.Request();
    }

    void PullResponsed(string Result)
    {
        Data = JsonMapper.ToObject<Data>(Result);
        Pulled();
    }

    public void Push()
    {
        IRequest Checker = new Upload(Data.UID, "/Accouts", ToJsonString(Data));
        Checker.AddOnResponsed(Pushed);
        Checker.Request();
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
public class Data
{
    public string UID;
    public int Win;
    public int Round;
    public int Coins;
    public string LinkingGame;

    public Data()
    {
        GenUID();
    }

    public Data(string _ID)
    {
        UID = _ID;
    }

    public void GenUID()
    {
        UID = DateTime.Now.Ticks.ToString("x");
    }

    public bool isNotLinking()
    {

        return (string.IsNullOrEmpty(LinkingGame));
    }
}

public class LitData
{
    public string UID;
    public LitData(Data _Data)
    {
        UID = _Data.UID;
    }
}