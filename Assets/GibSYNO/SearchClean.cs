using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class SearchClean : Search
    {
        public string taskid;
        public SearchClean(string _taskid)
        {
            taskid = _taskid;
        }

        protected override string GetMethod()
        {
            return "clean";
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