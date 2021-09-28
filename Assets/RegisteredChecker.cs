using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GibSYNO;
using LitJson;
using System.IO;
using System;

public class RegisteredChecker : MonoBehaviour
{
    [SerializeField] bool IsLoginByAdmin;
    [SerializeField] Data Data;
    string LocalPath;
    // Start is called before the first frame update
    void Start()
    {
        My_SYNO.InitServer();
        LocalPath = Application.persistentDataPath + "/LocalPlayerID.txt";
        Invoke("StartVerify", 2);
    }

    void StartVerify()
    {
        if (IsLoginByAdmin)
        {
            Data.UID = "Admin";
            RequestCheckRegistered();
            return;
        }

        print(LocalPath);
        if (System.IO.File.Exists(LocalPath))
        {
            string Result = System.IO.File.ReadAllText(LocalPath);
            JsonData jsonData = JsonMapper.ToObject(Result);
            Data.UID = jsonData["UID"].ToString();
            RequestCheckRegistered();
        }
        else
        {
            OnNotRegistered();
        }
    }

    void RequestCheckRegistered()
    {
        IRequest Checker = new List("/Accouts");
        Checker.AddOnResponsed(CheckRegisteredResponsed);
        Checker.Request();
    }
    void CheckRegisteredResponsed(string Result)
    {
        print(Result);
        JsonData jsonData = JsonMapper.ToObject(Result);
        if (jsonData["success"].ToString() == "True")
        {
            if (IsRegistered(Result))
            {
                OnRegistered();
            }
            else
            {
                OnNotRegistered();
            }
        }
    }
    bool IsRegistered(string Result)
    {
        JsonData jsonData = JsonMapper.ToObject(Result);
        int total = int.Parse(jsonData["data"]["total"].ToString());
        for (int i = 0; i < total; i++)
        {
            if (jsonData["data"]["files"][i]["name"].ToString() == Data.UID + ".txt")
            {
                return true;
            }
        }
        return false;
    }



    void WriteIDToLocal()
    {
        StreamWriter writer = new StreamWriter(LocalPath, false);
        writer.WriteLine(ToJsonString(new LitData(Data)));
        writer.Close();
    }
    string ToJsonString(object obj)
    {
        JsonWriter writer = new JsonWriter();
        JsonMapper.ToJson(obj, writer);
        return writer.ToString();
    }

    void RequestRegisterID()
    {
        IRequest Checker = new Upload(Data.UID, "/Accouts", ToJsonString(Data));
        Checker.Request();
    }

    void RequestDownloadData()
    {
        IRequest Checker = new Download(Data.UID, "/Accouts");
        Checker.AddOnResponsed(DownloadDataResponsed);
        Checker.Request();
    }

    void DownloadDataResponsed(string Result)
    {
        Data = JsonMapper.ToObject<Data>(Result);
        OnGetPlayerDataFromServer();
    }

    void OnRegistered()
    {
        RequestDownloadData();
    }

    void OnGetPlayerDataFromServer()
    {
        Destroy(gameObject);
    }

    void OnNotRegistered()
    {
        Data = new Data();
        WriteIDToLocal();
        RequestRegisterID();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PlayerData.instance.Data = Data;
        StatusChanger.instance.ChangeToLobby();
    }
}
