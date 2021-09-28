using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;
using System.Text;
using LitJson;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using UnityEngine.Events;

namespace GibSYNO
{

    public static class My_SYNO
    {
        public static Dictionary<string, BaseElement> APIElementDict = new Dictionary<string, BaseElement>();
        static string HomePath = "/home";
        static string AppPath = "/NASMulti";
        public static string BasePath
        {
            get
            {
                return HomePath + AppPath;
            }
        }

        // static string DumpLink = "https://ptsv2.com/t/zcds1-1632081460/post";
        static string IP = "gibgibnas.synology.me";
        static string Port = "5001";
        public static string DID = "";
        public static string SID = "";
        public static void InitServer()
        {
            APIElementDict.Add("info", new BaseElement("SYNO.API.Info"));
            APIElementDict.Add("auth", new BaseElement("SYNO.API.Auth"));
            APIElementDict.Add("download", new BaseElement("SYNO.FileStation.Download"));
            APIElementDict.Add("upload", new BaseElement("SYNO.FileStation.Upload"));
            APIElementDict.Add("createfolder", new BaseElement("SYNO.FileStation.CreateFolder"));
            APIElementDict.Add("delete", new BaseElement("SYNO.FileStation.Delete"));
            APIElementDict.Add("search", new BaseElement("SYNO.FileStation.Search"));
            APIElementDict.Add("favorite", new BaseElement("SYNO.FileStation.Favorite"));
            APIElementDict.Add("list", new BaseElement("SYNO.FileStation.List"));
            IRequest info = new Info();
            IRequest Auth = new Auth();
            info.AddOnResponsed(Auth.Request);

            info.Request();
        }

        public static string GetLink(string path, string API, string APIVersion, string Method, string[] Params)
        {
            string LinkWithOutParams = ShortLink + path + "?api=" + API + "&version=" + APIVersion + ((Method == "") ? "" : "&method=" + Method);
            string LinkParams = LinkMixParams(LinkWithOutParams, Params);
            return LinkParams + ((SID == "") ? "" : "&_sid=" + SID);
        }

        static string ShortLink
        {
            get
            {
                return "https://" + IP + ":" + Port + "/webapi/";
            }
        }

        static string LinkMixParams(string Link, string[] Params)
        {
            string _Link = Link;
            foreach (var item in Params)
            {
                _Link += "&" + item;
            }
            return _Link;
        }

    }

}