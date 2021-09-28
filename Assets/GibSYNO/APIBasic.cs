using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace GibSYNO
{

    public abstract class APIBasic : IRequest
    {
        Action<string> OnResponed_String;
        Action OnResponed;

        protected abstract BaseElement GetBaseElement();
        protected abstract string GetMethod();

        protected virtual string[] GetParams()
        {
            return new string[] { };
        }

        protected string Link
        {
            get
            {
                return My_SYNO.GetLink(GetBaseElement().cgi, GetBaseElement().API, GetBaseElement().version, GetMethod(), GetParams());
            }
        }

        public abstract void Request();
        protected virtual void Responsed(string Response)
        {
            if (OnResponed_String != null)
            {
                OnResponed_String(Response);
            }
            if (OnResponed != null)
            {
                OnResponed();
            }
        }

        public void AddOnResponsed(Action<string> _OnResponed)
        {
            OnResponed_String += _OnResponed;
        }
        public void AddOnResponsed(Action _OnResponed)
        {
            OnResponed += _OnResponed;
        }

    }


    public interface IRequest
    {
        void AddOnResponsed(Action<string> _OnResponed);
        void AddOnResponsed(Action _OnResponed);
        void Request();
    }



    public class BaseElement
    {
        public string API;
        public string cgi;
        public string version;

        public BaseElement(string _API)
        {
            API = _API;
        }
    }
}
