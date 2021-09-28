using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Transform Transform;

    static public float shakeDuration = 0f;
    static public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    float OriginalDuration;
    float OriginalAmount;


    public void StartShake(float _shakeDuration, float _shakeAmount)
    {
        OriginalDuration = _shakeDuration;
        OriginalAmount = _shakeAmount;
        shakeDuration = _shakeDuration;
        shakeAmount = _shakeAmount;
    }

    void Awake()
    {
        if (Transform == null)
        {
            Transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = Transform.localPosition;
    }

    void Update()
    {
        Transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
        shakeAmount = OriginalAmount * Mathf.InverseLerp(0, OriginalDuration, shakeDuration);
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeAmount = 0f;
            shakeDuration = 0f;
            OriginalDuration = 0f;
            Transform.localPosition = originalPos;
        }
    }
}
