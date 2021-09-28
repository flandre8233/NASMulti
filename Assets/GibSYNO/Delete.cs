using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class Delete : FileControl
    {
        public Delete()
        {
        }
        public Delete(string _Name, string _path) : base(_Name, _path)
        {
        }

        protected override BaseElement GetBaseElement()
        {
            return My_SYNO.APIElementDict["delete"];
        }

        protected override string GetMethod()
        {
            return "delete";
        }

        protected override string[] GetParams()
        {
            return new string[] { "path=" + GetFinalPath() + ((Name == "") ? "" : "/" + Name + ".txt") };
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