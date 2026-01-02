using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public AudioSource explosionSound;

    public Animator headAnimator;

    public AudioSource riser;

    public void GameOver()
    {
        GameManager.instance.DisplayGameOver();
    }

    public void Explosion()
    {
        riser.Stop();
        explosionSound.Play();
        headAnimator.Play("HeadFall");
        GameManager.instance.deathEffect.SetActive(true);
    }
}