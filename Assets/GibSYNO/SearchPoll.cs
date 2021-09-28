using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
namespace GibSYNO
{
    public class SearchPoll : PollRequest
    {
        string path;
        public string taskid;
        Action<string> OnPoll;

        public static void Create(string _path, Action<string> _OnPoll)
        {
            GameObject SpawnObject = new GameObject("SearchPoll");
            SearchPoll Component = SpawnObject.AddComponent<SearchPoll>();
            Component.path = _path;
            Component.OnPoll = _OnPoll;
        }

        private void Start()
        {
            IRequest Start = new SearchStart("");
            Start.AddOnResponsed(OnStartResponsed);
            Start.Request();
        }

        void OnStartResponsed(string Result)
        {
            StartResponsedToTaskId(Result);
            Invoke("List", 0.1f);
        }

        void StartResponsedToTaskId(string Result)
        {
            print(Result);
            JsonData jsonData = JsonMapper.ToObject(Result);
            if (jsonData["success"].ToString() == "True")
            {
                taskid = jsonData["data"]["taskid"].ToString();
            }
            else
            {
                Debug.LogError("Can't Success : " + Result);
            }

        }

        void List()
        {
            IRequest List = new SearchList(taskid);
            List.AddOnResponsed(OnListResponsed);
            List.Request();
        }

        void OnListResponsed(string Result)
        {
            if (OnPoll != null)
            {
                OnPoll(Result);
            }

            if (IsListFinished(Result))
            {
                Clean();
            }
            else
            {
                List();
            }
        }

        bool IsListFinished(string Result)
        {
            JsonData jsonData = JsonMapper.ToObject(Result);
            if (jsonData["success"].ToString() == "True")
            {
                return (jsonData["data"]["finished"].ToString() == "True");
            }
            else
            {
                Debug.LogError("Can't Success : " + Result);
            }
            return false;
        }

        void Clean()
        {
            IRequest Clean = new SearchClean(taskid);
            Clean.AddOnResponsed(OnCleanResponsed);
            Clean.Request();
        }

        void OnCleanResponsed(string Result)
        {
            Destroy(gameObject);
        }

    }

}
