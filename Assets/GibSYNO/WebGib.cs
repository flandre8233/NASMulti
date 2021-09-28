using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace GibSYNO
{
    public abstract class WebGib : MonoBehaviour
    {
        protected string url;
        protected System.Action<string> onMessageReceived;
        protected UnityWebRequest request;

        void Start()
        {
            SetUpWebRequest();
            StartCoroutine(WebAction());
        }

        protected abstract void SetUpWebRequest();

        IEnumerator WebAction()
        {
            string Response;
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            Response = request.downloadHandler.text;
            if (onMessageReceived != null)
            {
                onMessageReceived(Response);
            }

            Destroy(gameObject);
        }
    }
}
