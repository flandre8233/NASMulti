using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
namespace GibSYNO
{
    public class WebPOST : WebGib
    {
        List<IMultipartFormSection> formData;

        public static void Create(string ObjectName, string _url, List<IMultipartFormSection> _formData, System.Action<string> _onMessageReceived)
        {
            GameObject SpawnObject = new GameObject(typeof(WebPOST).ToString() + " : " + ObjectName);
            WebPOST Component = SpawnObject.AddComponent<WebPOST>();
            Component.url = _url;
            Component.onMessageReceived = _onMessageReceived;
            Component.formData = _formData;
        }

        protected override void SetUpWebRequest()
        {
            string boundary = DateTime.Now.Ticks.ToString("x");
            string form = ToMultiformStr(formData, boundary);
            request = new UnityWebRequest(url, "POST");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(form));
            request.uploadHandler.contentType = "multipart/form-data; boundary=" + boundary;
            // print(System.Text.Encoding.UTF8.GetString(request.uploadHandler.data));
        }

        string ToMultiformStr(List<IMultipartFormSection> formSection, string boundary)
        {
            string Result = "";
            string boundaryLines = "\r\n--" + boundary + "\r\n";
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}";
            string trailer = "\r\n--" + boundary + "--";

            foreach (var item in formSection)
            {
                Result += boundaryLines;
                string Name = item.sectionName;
                string Value = System.Text.Encoding.UTF8.GetString(item.sectionData);
                string contentType = item.contentType;
                string fileName = item.fileName;
                string FormLines = string.Format(formdataTemplate, Name, Value);
                string FileLines = string.Format(headerTemplate, Name, fileName, contentType, Value + "==");
                Result += (item.GetType() == typeof(MultipartFormDataSection)) ? FormLines : FileLines;
            }
            Result += trailer;
            Result = Result.Substring(Math.Max(0, 2));
            return Result;
        }
    }
}