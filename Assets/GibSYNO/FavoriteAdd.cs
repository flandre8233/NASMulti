using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class FavoriteAdd : Favorite
    {
        public string name;
        public string path;
        public FavoriteAdd(string _path, string _name)
        {
            path = _path;
            name = _name;
        }

        protected override string GetMethod()
        {
            return "add";
        }

        protected override string[] GetParams()
        {
            return new string[] { "path=" + GetFinalPath(), "name=" + name };
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }

        string GetFinalPath()
        {
            return My_SYNO.BasePath + path;
        }
    }
}
