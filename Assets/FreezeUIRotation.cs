using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeUIRotation : MonoBehaviour
{
    public RectTransform rectTransform;
    // Update is called once per frame
    void Update()
    {
        rectTransform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
