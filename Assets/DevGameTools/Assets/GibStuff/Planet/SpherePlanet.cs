using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePlanet : MonoBehaviour
{
    [SerializeField] int Radius;

    private void Start()
    {
        Radius = Mathf.RoundToInt(transform.localScale.y / 2);
    }

    //95 100 105
    public Vector3 SphericalToCartesian_SurfaceOnly(float polar, float elevation)
    {
        return PolarToV3(Radius - 2, polar, elevation);
    }

    public Vector3 PolarToV3(float radius, float angle, float elevation)
    {
        Vector3 outCart;

        float angleRad = (Mathf.PI / 180.0f) * (angle - 90f);
        float elevationRad = (Mathf.PI / 180.0f) * (elevation - 90f);


        float a = radius * Mathf.Cos(elevationRad);
        outCart.x = a * Mathf.Cos(angleRad);
        outCart.y = radius * Mathf.Sin(elevationRad);
        outCart.z = a * Mathf.Sin(angleRad);
        outCart += transform.localPosition;

        return outCart;
    }



    public Vector3 V3ToPolar(Vector3 cartCoords)
    {
        float outRadius;
        float outPolar;
        float outElevation;
        V3ToPolar(cartCoords, out outRadius, out outPolar, out outElevation);

        return new Vector3(outRadius, outPolar, outElevation);
    }

    public void V3ToPolar(Vector3 cartCoords, out float outRadius, out float outPolar, out float outElevation)
    {
        cartCoords -= transform.localPosition;

        if (cartCoords.x == 0)
            cartCoords.x = Mathf.Epsilon;

        outRadius = Mathf.Sqrt((cartCoords.x * cartCoords.x)
                        + (cartCoords.y * cartCoords.y)
                        + (cartCoords.z * cartCoords.z));
        outPolar = Mathf.Atan(cartCoords.z / cartCoords.x);
        if (cartCoords.x < 0)
            outPolar += Mathf.PI;
        outElevation = Mathf.Asin(cartCoords.y / outRadius);

        outPolar = (outPolar / (Mathf.PI / 180)) + 90;
        outElevation = (outElevation / (Mathf.PI / 180)) + 90;
    }


}