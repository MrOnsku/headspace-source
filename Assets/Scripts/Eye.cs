using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public float tearShootingRate;
    private float curTearShootingTime;

    public string targetTag = "SpeechBubble";
    public GameObject targetObject;
    public GameObject[] otherObjects;

    public GameObject tear;

    private GameObject closestObject;

    public Emotion sadness;

    public AudioSource shootSound;

    private void Start()
    {
        curTearShootingTime = tearShootingRate;
    }

    void Update()
    {
        tearShootingRate = GameManager.instance.tearFireRate;

        if(sadness.hp != 0)
        {
            FindClosestObject();

            if (curTearShootingTime > 0 && closestObject != null)
            {
                curTearShootingTime -= Time.deltaTime;
            }

            if (curTearShootingTime <= 0)
            {
                if (closestObject != null)
                {
                    ShootTear();
                    curTearShootingTime = tearShootingRate;
                }
            }
        }
    }

    void FindClosestObject()
    {
        otherObjects = GameObject.FindGameObjectsWithTag(targetTag);

        if (targetObject == null || otherObjects.Length == 0)
        {
            return;
        }

        float minDistance = Mathf.Infinity;
        closestObject = null;

        foreach (GameObject otherObject in otherObjects)
        {
            float distance = Vector3.Distance(targetObject.transform.position, otherObject.transform.position);

            if (distance < minDistance && otherObject.CompareTag(targetTag))
            {
                minDistance = distance;
                closestObject = otherObject;
            }
        }
    }

    void ShootTear()
    {
        if (closestObject.GetComponent<SpeechBubble>().shootable)
        {
            shootSound.Play();
            Instantiate(tear, transform.position, Quaternion.identity).GetComponent<Tear>().target = closestObject;
        }
    }
}