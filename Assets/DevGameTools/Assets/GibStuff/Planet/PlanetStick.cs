using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStick : MonoBehaviour
{
    [SerializeField] SpherePlanet planet;
    Transform target;

    private void Start()
    {
        target = planet.transform;
    }

    void Update()
    {
        Vector3 gravityUp = (transform.position - target.position).normalized;
        Vector3 localUp = transform.transform.up;

        // Allign bodies up axis with the centre of planet
        transform.rotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
    }
}