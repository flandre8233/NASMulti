using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public class Logout : AuthBase
    {
        protected override string GetMethod()
        {
            return "logout";
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }


    }

}