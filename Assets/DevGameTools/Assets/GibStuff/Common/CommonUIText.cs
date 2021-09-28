using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class CommonUIText : MonoBehaviour
{
    [SerializeField] protected Text text;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        text = GetComponent<Text>();
    }

    public abstract void UpdateText();

}
