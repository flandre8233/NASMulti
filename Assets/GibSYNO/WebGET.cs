using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace GibSYNO
{
    public class WebGET : WebGib
    {
        public static void Create(string ObjectName , string _url, System.Action<string> _onMessageReceived)
        {
            GameObject SpawnObject = new GameObject(typeof(WebGET).ToString() + " : " + ObjectName);
            WebGET Component = SpawnObject.AddComponent<WebGET>();
            Component.url = _url;
            Component.onMessageReceived = _onMessageReceived;
        }

        protected override void SetUpWebRequest()
        {
            request = new UnityWebRequest(url, "GET");
        }
    }

}