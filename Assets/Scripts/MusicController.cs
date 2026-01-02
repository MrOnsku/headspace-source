using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    public AudioSource BG;
    public AudioSource det;
    public AudioSource joy;
    public AudioSource sad;

    public Emotion _det;
    public Emotion _joy;
    public Emotion _sad;

    public float layersBaseVolume = 0;

    private void Start()
    {
        instance = this;

        det.time = BG.time;
        sad.time = BG.time;
        joy.time = BG.time;
    }

    private void Update()
    {
        if (!BG.isPlaying)
        {
            BG.Play();
        }

        float baseVolume = layersBaseVolume * 0.18f;

        det.volume = Mathf.Clamp01((float)_det.hp / _det.maxhp) * baseVolume;
        joy.volume = Mathf.Clamp01((float)_joy.hp / _joy.maxhp) * baseVolume;
        sad.volume = Mathf.Clamp01((float)_sad.hp / _sad.maxhp) * baseVolume;

        if (GameManager.instance.gameRunning)
        {
            if(layersBaseVolume < 1)
            {
                layersBaseVolume += Time.deltaTime / 2;
            }
        }
        else
        {
            if (layersBaseVolume > 0)
            {
                layersBaseVolume -= Time.deltaTime / 2;
            }
        }

        if(layersBaseVolume > 1)
        {
            layersBaseVolume = 1;
        }

        if(layersBaseVolume < 0)
        {
            layersBaseVolume = 0;
        }

        if (GameManager.instance.blackBg.activeInHierarchy)
        {
            BG.pitch -= Time.deltaTime / 4;
        }

        if(BG.pitch < 0)
        {
            BG.pitch = 0;
        }
    }
}