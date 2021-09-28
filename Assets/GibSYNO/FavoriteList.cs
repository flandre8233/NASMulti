using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class FavoriteList : Favorite
    {
        public FavoriteList()
        {
        }

        protected override string GetMethod()
        {
            return "list";
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }
    }
}
