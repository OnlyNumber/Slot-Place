using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [ContextMenu("Check")]
    public void Check()
    {
        RectTransform rect;

        rect = GetComponent<RectTransform>();

        Debug.Log("Camera 0 " + rect.rect.height);
    }
}
