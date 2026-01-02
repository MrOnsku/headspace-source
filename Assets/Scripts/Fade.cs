using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    private Image image;

    private Animator animator;

    private string scene;

    private void Start()
    {
        instance = this;

        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void FadeOut(string sceneTarget)
    {
        gameObject.SetActive(true);
        animator.Play("FadeOut");
        scene = sceneTarget;
    }

    public void FadedIn()
    {
        gameObject.SetActive(false);
    }

    public void FadedOut()
    {
        if(scene != null)
        {
            SceneManager.LoadScene(scene);
        }
    }
}