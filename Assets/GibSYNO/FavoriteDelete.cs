using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class FavoriteDelete : Favorite
    {
        public string path;
        public FavoriteDelete(string _path)
        {
            path = _path;
        }

        protected override string GetMethod()
        {
            return "delete";
        }

        protected override string[] GetParams()
        {
            return new string[] { "path=" + GetFinalPath() };
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
