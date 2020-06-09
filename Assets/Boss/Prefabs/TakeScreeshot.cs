using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreeshot : MonoBehaviour
{
#if UNITY_EDITOR

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(ScreenshotCoroutine());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Screeshot();
        }
    }

    IEnumerator ScreenshotCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Screeshot();
        }
    }

    void Screeshot()
    {
        ScreenCapture.CaptureScreenshot(string.Format("Build/Screenshots/SS_{0}.png", DateTime.Now.ToString("yyyyMMddHHmmss")));
    }

#endif
}
