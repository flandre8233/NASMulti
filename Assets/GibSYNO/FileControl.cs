using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public abstract class FileControl : APIBasic
    {
        public string Name;
        public string path;

        public FileControl()
        {
        }

        public FileControl(string _Name, string _path)
        {
            Name = _Name;
            path = _path;
        }

        protected string GetFinalPath()
        {
            return My_SYNO.BasePath + path;
        }

        protected bool AutoAuthIfNeeded()
        {
            if (My_SYNO.SID == "")
            {
                Debug.LogWarning("must get auth before " + typeof(Upload).ToString() + ", try getting server auth.");
                IRequest request = new Auth();
                request.Request();
                return true;
            }
            return false;
        }
    }

}