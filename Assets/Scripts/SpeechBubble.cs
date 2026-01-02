using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public float hp;
    [HideInInspector] public float curHp;

    public float speed;

    public bool shootable;

    public Slider hpbar;

    public GameObject hpbarObject;

    public GameObject stuff;

    [HideInInspector] public float shakeAmp;

    public Emotion[] emotions;

    public AudioSource damageSound;
    public AudioSource explosionSound;

    public ParticleSystem explosionParticles;

    private void Start()
    {
        hp = GameManager.instance.speechBubbleHp;
        curHp = hp;

        emotions.SetValue(GameObject.Find("Map").transform.GetChild(1).GetComponent<Emotion>(), 0);
        emotions.SetValue(GameObject.Find("Map").transform.GetChild(2).GetComponent<Emotion>(), 1);
        emotions.SetValue(GameObject.Find("Map").transform.GetChild(3).GetComponent<Emotion>(), 2);
    }

    private void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime,0));

        hpbar.value = (curHp / hp) * 100;

        if(curHp == hp)
        {
            hpbarObject.SetActive(false);
        }
        else
        {
            hpbarObject.SetActive(true);
        }

        if(curHp <= 0)
        {
            explosionParticles.gameObject.SetActive(true);
            explosionParticles.Emit(30);
            explosionParticles.gameObject.transform.parent = null;
            explosionSound.gameObject.SetActive(true);
            explosionSound.transform.parent = null;
            explosionSound.Play();
            Destroy(gameObject);
        }

        if(shakeAmp > 0)
        {
            shakeAmp -= Time.deltaTime / 2;
        }

        if(shakeAmp < 0)
        {
            shakeAmp = 0;
        }

        if(shakeAmp == 0)
        {
            stuff.transform.localPosition = Vector3.Lerp(stuff.transform.localPosition, new Vector3(0,0,0), Time.deltaTime * 8);
        }

        if(hp > 12)
        {
            hp = 12;
        }

        stuff.transform.localPosition = new Vector3(stuff.transform.localPosition.x + Random.Range(-shakeAmp, shakeAmp), stuff.transform.localPosition.y + Random.Range(-shakeAmp, shakeAmp), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Head")
        {
            collision.gameObject.GetComponent<Animator>().Play("HeadDamage");

            collision.gameObject.transform.GetChild(2).GetComponent<AudioSource>().Play();

            if (emotions[2].hp != 0)
            {
                emotions[Random.Range(0, emotions.Length)].hp -= (emotions[0].maxhp / 3);
            }
            else
            {
                emotions[Random.Range(0, emotions.Length)].hp -= (emotions[0].maxhp / 2);
            }

            Destroy(gameObject);
        }

        if(collision.gameObject.name == "ViewArea")
        {
            shootable = true;
        }
    }
}