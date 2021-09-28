using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class CreateFolder : FileControl
    {
        public CreateFolder()
        {

        }
        public CreateFolder(string _Name, string _path) : base(_Name, _path)
        {
        }

        protected override BaseElement GetBaseElement()
        {
            return My_SYNO.APIElementDict["createfolder"];
        }
        protected override string GetMethod()
        {
            return "create";
        }

        protected override string[] GetParams()
        {
            return new string[] { "folder_path=" + GetFinalPath(), "name=" + Name };
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }


    }
}