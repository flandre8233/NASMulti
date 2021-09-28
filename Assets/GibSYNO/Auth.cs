using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
namespace GibSYNO
{
    public class Auth : AuthBase
    {
        static string Accout = "API_User";
        static string Password = "PU^U^<";
        protected override string GetMethod()
        {
            return "login";
        }
        protected override string[] GetParams()
        {
            return new string[] { "account=" + Accout, "passwd=" + Password, "session=FileStation", "format=sid" };
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }

        protected override void Responsed(string Response)
        {
            JsonData jsonData = JsonMapper.ToObject(Response);
            My_SYNO.DID = jsonData["data"]["device_id"].ToString();
            My_SYNO.SID = jsonData["data"]["sid"].ToString();
            base.Responsed(Response);
        }
    }
}
