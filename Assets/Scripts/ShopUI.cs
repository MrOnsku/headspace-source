using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;

    public Animator animator;

    public bool exiting;

    private void Start()
    {
        instance = this;

        animator = GetComponent<Animator>();
    }

    public void ExitedShop()
    {
        gameObject.SetActive(false);
    }
}