using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GibSYNO
{
    public abstract class Search : APIBasic
    {
        protected override BaseElement GetBaseElement()
        {
            return My_SYNO.APIElementDict["search"];
        }
    }
}