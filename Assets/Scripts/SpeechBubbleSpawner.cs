using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleSpawner : MonoBehaviour
{
    public static SpeechBubbleSpawner instance;

    public int bubblesToSpawn;
    public int setBubblesToSpawn;

    public float bubbleSpawnTimer;
    private float curBubbleSpawnTimer;

    public GameObject[] bubbles;

    private void Start()
    {
        instance = this;

        curBubbleSpawnTimer = bubbleSpawnTimer;

        setBubblesToSpawn = bubblesToSpawn;
    }

    private void Update()
    {
        if (GameManager.instance.gameRunning)
        {
            if(curBubbleSpawnTimer > 0 && bubblesToSpawn > 0)
            {
                curBubbleSpawnTimer -= Time.deltaTime;
            }

            if(curBubbleSpawnTimer <= 0)
            {
                SpawnBubble();
                curBubbleSpawnTimer = bubbleSpawnTimer + Random.Range(0.2f, 1.7f);
                bubblesToSpawn--;
            }
        }

        if(bubbleSpawnTimer < 0.4f)
        {
            bubbleSpawnTimer = 0.4f;
        }
    }

    public void SpawnBubble()
    {
        Instantiate(bubbles[Random.Range(0,bubbles.Length)], new Vector3(transform.position.x, Random.Range(-4.29f, 4.29f), 0), Quaternion.identity);
    }
}