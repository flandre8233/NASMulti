using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class SearchStart : Search
    {
        public string path;
        public SearchStart(string _path)
        {
            path = _path;
        }

        protected override string GetMethod()
        {
            return "start";
        }

        protected override string[] GetParams()
        {
            return new string[] { "folder_path=" + GetFinalPath() , "pattern=1" };
        }

        public override void Request()
        {
            Debug.Log("SearchPath : " + GetFinalPath());
            WebGET.Create(GetType().Name, Link, Responsed);
        }

        string GetFinalPath()
        {
            return My_SYNO.BasePath + path;
        }
    }
}