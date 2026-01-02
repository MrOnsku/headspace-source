using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
    public Vector3 offset;

    void LateUpdate()
    {
        transform.localPosition = offset;
    }
}