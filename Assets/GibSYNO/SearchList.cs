using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class SearchList : Search
    {
        public string taskid;
        public SearchList(string _taskid)
        {
            taskid = _taskid;
        }

        protected override string GetMethod()
        {
            return "list";
        }

        protected override string[] GetParams()
        {
            return new string[] { "taskid=" + taskid };
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }
    }
}