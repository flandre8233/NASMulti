using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float PlanetRotateSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, PlanetRotateSpeed * Time.deltaTime));
    }
}
