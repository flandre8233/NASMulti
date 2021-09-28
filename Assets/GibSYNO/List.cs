using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class List : APIBasic
    {
        string path;
        public List(string _path)
        {
            path = _path;
        }
        protected override BaseElement GetBaseElement()
        {
            return My_SYNO.APIElementDict["list"];
        }

        protected override string GetMethod()
        {
            return "list";
        }
        protected override string[] GetParams()
        {
            return new string[] { "folder_path=" + GetFinalPath() };
        }

        public override void Request()
        {
            Debug.Log(Link);
            WebGET.Create(GetType().Name, Link, Responsed);
        }

        string GetFinalPath()
        {
            return My_SYNO.BasePath + path;
        }
    }
}