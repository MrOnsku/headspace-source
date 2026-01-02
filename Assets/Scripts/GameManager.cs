using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject mainMenu;
    public GameObject gameUI;

    public bool gameRunning;

    public int wave;

    public TMP_Text waveDisplay;
    public TMP_Text grayMatterDisplay;
    public GameObject grayMatterDisplayObject;

    public GrayMatterSpawn[] grayMatterSpawns;

    public int grayMatter = 0;

    public float grayMatterSpawnTime;
    private float curGrayMatterSpawnTime;

    public Vector3 grayMatterDisplayMainPos;

    public float shopAppearTime;
    private float curShopAppearTime;

    public GameObject shopUI;

    public Emotion[] emotions;

    public float tearDamage = 1;

    public float tearFireRate = 1.8f;

    public AudioSource buttonSound;
    public AudioSource buySound;
    public AudioSource declineSound;

    public GameObject gameOverUI;

    public float speechBubbleHp = 1;

    public float enemySpawnTime;
    private float curEnemySpawnTime;

    public EnemySpawn[] enemySpawns;

    public Animator[] brainAnimators;

    public GameObject tutorialUI;

    public GameObject[] bubbles;

    public GameObject blackBg;

    private bool noJoyFadeOut;

    public SpriteRenderer noJoyOverlay;

    public GameObject deathEffect;

    public Animator menuAnimator;

    private bool started;

    public Animator grayMatterDisplayAnimator;
    
    private void Start()
    {
        instance = this;

        grayMatterDisplayObject.SetActive(false);
        curGrayMatterSpawnTime = grayMatterSpawnTime + Random.Range(-1.5f, 1.5f);
        grayMatterDisplayMainPos = grayMatterDisplayObject.transform.localPosition;
        curShopAppearTime = shopAppearTime;
        curEnemySpawnTime = enemySpawnTime;
    }

    private void Update()
    {
        bubbles = GameObject.FindGameObjectsWithTag("SpeechBubble");

        if (gameRunning)
        {
            gameUI.SetActive(true);
            grayMatterDisplayObject.SetActive(true);
            waveDisplay.text = "Wave:" + wave;

            if(curGrayMatterSpawnTime > 0)
            {
                curGrayMatterSpawnTime -= Time.deltaTime;
            }

            if (curEnemySpawnTime > 0)
            {
                curEnemySpawnTime -= Time.deltaTime;
            }

            if (curGrayMatterSpawnTime <= 0)
            {
                grayMatterSpawns[Random.Range(0, grayMatterSpawns.Length)].Spawn();

                curGrayMatterSpawnTime = grayMatterSpawnTime + Random.Range(-1.5f,1.5f);
            }

            if (curEnemySpawnTime <= 0)
            {
                enemySpawns[Random.Range(0, enemySpawns.Length)].Spawn();

                curEnemySpawnTime = enemySpawnTime + Random.Range(-1.5f, 1.5f);
            }

            if (SpeechBubbleSpawner.instance.bubblesToSpawn <= 0 && GameObject.FindGameObjectsWithTag("SpeechBubble").Length == 0)
            {
                curShopAppearTime -= Time.deltaTime;
            }

            if(curShopAppearTime <= 0 && GameObject.FindGameObjectsWithTag("SpeechBubble").Length == 0)
            {
                shopUI.SetActive(true);
                ShopUI.instance.exiting = false;
                gameUI.SetActive(false);
                gameRunning = false;
            }

            if (emotions[0].hp == 0 && emotions[1].hp == 0 && emotions[2].hp == 0)
            {
                GameOver();
            }
        }

        if (noJoyFadeOut)
        {
            noJoyOverlay.color = new Color(noJoyOverlay.color.r, noJoyOverlay.color.g, noJoyOverlay.color.b, Mathf.Lerp(noJoyOverlay.color.a, 0, Time.deltaTime * 8));
        }

        grayMatterDisplay.text = ":" + grayMatter.ToString("D3");

        if (tearFireRate < 0.4f)
        {
            tearFireRate = 0.4f;
        }

        if(tearDamage > 3)
        {
            tearDamage = 3;
        }

       // grayMatterDisplayObject.transform.localPosition = Vector3.Lerp(grayMatterDisplayObject.transform.position, grayMatterDisplayMainPos, Time.deltaTime * 8);

        if (deathEffect.activeInHierarchy)
        {
            var spriteRen = deathEffect.GetComponent<SpriteRenderer>();

            if(spriteRen.color.a > 0.1f)
            {
                spriteRen.color = new Color(spriteRen.color.r, spriteRen.color.g, spriteRen.color.g, spriteRen.color.a - Time.deltaTime / 2);
            }
        }
    }

    public void StartGame()
    {
        if(started == false)
        {
            menuAnimator.Play("MenuHide");
            buttonSound.Play();
            gameRunning = true;
            started = true;
        }
    }

    public void NextWave()
    {
        if(ShopUI.instance.exiting == false)
        {
            ShopUI.instance.exiting = true;
            ShopUI.instance.animator.Play("ShopClose");

            buttonSound.Play();
            gameRunning = true;
            wave++;
            gameUI.SetActive(true);

            foreach (var item in emotions)
            {
                item.hpDecreaseTimer -= 0.03f;
            }

            speechBubbleHp += 0.04f;

            SpeechBubbleSpawner.instance.bubblesToSpawn = SpeechBubbleSpawner.instance.setBubblesToSpawn + 3;
            SpeechBubbleSpawner.instance.bubbleSpawnTimer = SpeechBubbleSpawner.instance.bubbleSpawnTimer -= 0.6f;

            curShopAppearTime = shopAppearTime;
        }
    }

    public void BackToMenu()
    {
        buttonSound.Play();
        Fade.instance.FadeOut("Main");
    }

    public void GameOver()
    {
        noJoyFadeOut = true;
        blackBg.SetActive(true);
        gameRunning = false;
        grayMatterDisplayObject.SetActive(false);
        gameUI.SetActive(false);

        foreach (var item in bubbles)
        {
            item.GetComponent<SpeechBubble>().curHp = 0;
        }

        foreach (var item in brainAnimators)
        {
            item.Play("BrainExplode");
        }
    }

    public void DisplayGameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void OpenTutorial()
    {
        if (started == false)
        {
            buttonSound.Play();
            tutorialUI.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        buttonSound.Play();
        tutorialUI.SetActive(false);
        mainMenu.SetActive(true);
    }
}