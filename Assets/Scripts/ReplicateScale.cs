using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplicateScale : MonoBehaviour
{
    public SpriteRenderer targetScale;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        spriteRenderer.size = targetScale.size;
    }
}