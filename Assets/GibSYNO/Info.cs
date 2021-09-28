using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
namespace GibSYNO
{
    public class Info : APIBasic
    {
        string _Param;

        public Info()
        {
            GetBaseElement().cgi = "query.cgi";
            GetBaseElement().version = "1";
            // _Param = "query=" + "ALL";
            _Param = "query=" + GetBaseElement().API;
            foreach (KeyValuePair<string, BaseElement> item in My_SYNO.APIElementDict)
            {
                BaseElement value = item.Value;
                _Param += ("," + value.API);
            }
        }

        protected override BaseElement GetBaseElement()
        {
            return My_SYNO.APIElementDict["info"];
        }

        protected override string GetMethod()
        {
            return "query";
        }

        protected override string[] GetParams()
        {
            return new string[] { _Param };
        }

        public override void Request()
        {
            WebGET.Create(GetType().Name, Link, Responsed);
        }

        protected override void Responsed(string Response)
        {
            JsonData jsonData = JsonMapper.ToObject(Response);

            foreach (KeyValuePair<string, BaseElement> item in My_SYNO.APIElementDict)
            {
                BaseElement value = item.Value;
                string API = value.API;
                value.cgi = (jsonData["data"][API]["path"].ToString());
                value.version = (jsonData["data"][API]["maxVersion"].ToString());
            }
            base.Responsed(Response);
        }

    }
}