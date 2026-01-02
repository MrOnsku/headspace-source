using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public EnemySightCheck radiusCheck;

    public Animator[] animators;

    public LayerMask obstacle;

    private bool shouldChase;
    private Vector2 direction;

    public ParticleSystem deathFx;

    public GameObject owner;

    public AudioSource attackSound;
    public AudioSource deathSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.instance.gameRunning)
        {
            direction = (Vector2)GameObject.Find("Player").transform.position - (Vector2)transform.position;
            direction.Normalize();

            RaycastHit2D sight = Physics2D.Raycast((Vector2)transform.position, direction, Mathf.Infinity, obstacle);

            shouldChase = radiusCheck.inRange && (sight.collider == null || sight.collider.gameObject == GameObject.Find("Player"));

            foreach (var item in animators)
            {
                item.SetBool("Chase", shouldChase);
            }
        }
        else
        {
            foreach (var item in animators)
            {
                item.SetBool("Chase", false);
            }
        }
    }
    public void AlertOwner()
    {
        owner.GetComponent<EnemySpawn>().spawned = false;
    }

    private void FixedUpdate()
    {
        if (shouldChase && GameManager.instance.gameRunning)
        {
            rb.AddForce(direction / 1.8f, ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(((Vector2)GameObject.Find("Player").transform.position - (Vector2)transform.position) * 1.2f, ForceMode2D.Impulse);
            attackSound.Play();
        }
    }
}