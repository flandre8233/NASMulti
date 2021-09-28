using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class Download : FileControl
    {
        public Download()
        {
        }
        public Download(string _Name, string _path) : base(_Name, _path)
        {
        }
        protected override BaseElement GetBaseElement()
        {
            return My_SYNO.APIElementDict["download"];
        }

        protected override string GetMethod()
        {
            return "download";
        }

        protected override string[] GetParams()
        {
            return new string[] { "path=" + GetFinalPath() + "/" + Name + ".txt", "mode=" + "download" };
        }

        public override void Request()
        {
            if (AutoAuthIfNeeded())
            {
                return;
            }
            WebGET.Create(GetType().Name, Link, Responsed);
        }


    }
}