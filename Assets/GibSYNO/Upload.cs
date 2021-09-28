using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
namespace GibSYNO
{
    public class Upload : FileControl
    {
        public string Content;
        public Upload()
        {

        }
        public Upload(string _Name, string _path, string _Content) : base(_Name, _path)
        {
            Content = _Content;

        }
        protected override BaseElement GetBaseElement()
        {
            return My_SYNO.APIElementDict["upload"];
        }

        protected override string GetMethod()
        {
            return "upload";
        }

        protected override string[] GetParams()
        {
            return new string[] { };
        }

        public override void Request()
        {
            if (AutoAuthIfNeeded())
            {
                return;
            }
            WebPOST.Create(GetType().Name, Link, GenForm(), Responsed);
        }
        List<IMultipartFormSection> GenForm()
        {
            byte[] postBytes = System.Text.Encoding.Default.GetBytes(Content);
            List<IMultipartFormSection> form = new List<IMultipartFormSection>();
            form.Add(new MultipartFormDataSection("path", GetFinalPath()));
            form.Add(new MultipartFormDataSection("create_parents", "true"));
            form.Add(new MultipartFormDataSection("overwrite", "overwrite"));
            form.Add(new MultipartFormFileSection("file", Encoding.UTF8.GetBytes(Content), Name + ".txt", "application/octet-stream"));
            return form;
        }
    }
}