using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time;

    public bool destroy;

    private void Update()
    {
        if(destroy == true)
        {
            time -= Time.deltaTime;
        }

        if(time <= 0 && destroy)
        {
            Destroy(gameObject);
        }
    }
}