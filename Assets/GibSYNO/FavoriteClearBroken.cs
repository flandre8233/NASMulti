using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class FavoriteClearBroken : Favorite
    {
        public FavoriteClearBroken()
        {
        }

        protected override string GetMethod()
        {
            return "clear_broken";
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }
    }
}