using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator[] animator;

    public float speed;
    public float dashSpeed;
    private float dashCooldown;

    private float inputX;
    private float inputY;
    private bool dash;

    private Vector2 movementDirection;

    public GameObject shadow;

    public ParticleSystem dashParticles;

    public SpriteRenderer dashFx;

    public bool dashing;

    private float dashKillTime;

    public TrailRenderer trail;

    public AudioSource dashSound;
    public AudioSource grayMatterCollectSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.instance.gameRunning)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
            dash = Input.GetKeyDown(KeyCode.X);
        }
        else
        {
            inputX = 0;
            inputY = 0;
            dash = false;
        }

        shadow.transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y - 0.1f);
        shadow.transform.rotation = transform.rotation;

        if (dash && dashCooldown <= 0)
        {
            if(inputX != 0 || inputY != 0)
            {
                rb.AddForce(new Vector2(inputX, inputY).normalized * dashSpeed, ForceMode2D.Impulse);
                dashParticles.Emit(20);
                dashCooldown = 2;
                dashKillTime = 0.4f;
                dashSound.Play();
            }
        }

        if(dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;

            dashFx.color = new Color(dashFx.color.r, dashFx.color.g, dashFx.color.b, dashCooldown / 3);
        }

        if(dashKillTime > 0)
        {
            dashKillTime -= Time.deltaTime;
        }

        if(dashKillTime > 0)
        {
            dashing = true;
        }
        else
        {
            dashing = false;
        }

        foreach (var item in animator)
        {
            item.SetBool("Dashing", dashing);
        }

        shadow.GetComponent<SpriteRenderer>().enabled = dashFx.GetComponent<SpriteRenderer>().enabled;
        trail.emitting = dashing;
    }

    private void FixedUpdate()
    {
        if(inputX != 0 || inputY != 0)
        {
            movementDirection = new Vector2(inputX, inputY).normalized;
        }

        float targetAngle = Mathf.Atan2(-movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
        float angle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.deltaTime * 8);
        rb.rotation = angle;

        rb.AddForce(new Vector2(inputX, inputY).normalized * speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if (dashing)
            {
                var enemy = collision.gameObject.GetComponent<Enemy>();

                enemy.AlertOwner();
                enemy.deathSound.gameObject.SetActive(true);
                enemy.deathSound.gameObject.transform.parent = null;
                enemy.deathFx.Emit(20);
                enemy.deathFx.GetComponent<DestroyAfterTime>().destroy = true;
                enemy.deathFx.transform.parent = null;
                Destroy(collision.gameObject);
            }
        }
    }
}