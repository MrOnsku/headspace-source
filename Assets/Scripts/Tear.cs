using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tear : MonoBehaviour
{
    public GameObject target;

    private Vector3 targetPos;

    public float speed = 10;

    public float arcHeight = 1;

    Vector3 startPos;

    public float damage;

    void Start()
    {
        startPos = transform.position;
        targetPos = new Vector3(target.transform.position.x + 2, target.transform.position.y, target.transform.position.z);
        damage = GameManager.instance.tearDamage;
    }

    void Update()
    {
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        var nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;

        if (nextPos == targetPos) Arrived();
    }

    void Arrived()
    {
        Destroy(gameObject);
    }

    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "SpeechBubble")
        {
            collision.gameObject.GetComponent<SpeechBubble>().curHp -= damage;
            collision.gameObject.GetComponent<SpeechBubble>().shakeAmp = 0.08f;
            collision.gameObject.GetComponent<SpeechBubble>().damageSound.Play();
            Destroy(gameObject);
        }
    }
}