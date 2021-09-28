using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleForward : MonoBehaviour
{
    public float Speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Speed;
    }
}
