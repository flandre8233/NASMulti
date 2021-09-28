using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class CommonUIButton : MonoBehaviour
{
    protected Button button;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { Click(); });
    }

    protected abstract void Click();
}
