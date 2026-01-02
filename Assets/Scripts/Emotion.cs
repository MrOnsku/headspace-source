using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Emotion : MonoBehaviour
{
    public TMP_Text hpDisplay;

    public int hp;
    public int maxhp = 100;

    public float hpDecreaseTimer = 0.7f;
    private float curHpDecreaseTimer;
    private float healTimer = 0.3f;

    private bool healing = false;

    private void Start()
    {
        hp = maxhp;
        curHpDecreaseTimer = hpDecreaseTimer;
    }

    private void Update()
    {
        hpDisplay.text = hp.ToString();

        if (GameManager.instance.gameRunning && !healing)
        {
            if (curHpDecreaseTimer > 0)
            {
                curHpDecreaseTimer -= Time.deltaTime;
            }

            if (curHpDecreaseTimer <= 0)
            {
                hp--;
                curHpDecreaseTimer = hpDecreaseTimer;
            }
        }

        if (healing && GameManager.instance.gameRunning)
        {
            if(healTimer > 0)
            {
                healTimer -= Time.deltaTime;
            }

            if(healTimer <= 0)
            {
                hp++;
                healTimer = 0.1f;
            }
        }

        if(hp > maxhp)
        {
            hp = maxhp;
        }

        if(hp < 0)
        {
            hp = 0;
        }

        if(maxhp > 200)
        {
            maxhp = 200;
        }

        if(hpDecreaseTimer < 0.45f)
        {
            hpDecreaseTimer = 0.45f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            healing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            healing = false;
        }
    }
}