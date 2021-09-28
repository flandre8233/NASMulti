using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeControll : SingletonMonoBehavior<ShakeControll>
{
    [SerializeField] Shake Camera;
    [SerializeField] Shake GameCanvas;

    public static void CameraStart(float _shakeDuration, float _shakeAmount)
    {
        instance.Camera.StartShake(_shakeDuration, _shakeAmount);
    }

    public static void GameCanvasStart(float _shakeDuration, float _shakeAmount)
    {
        instance.GameCanvas.StartShake(_shakeDuration, _shakeAmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            CameraStart(999f, 0.2f);
        }
    }
}
