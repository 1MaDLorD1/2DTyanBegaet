using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField]
    private int lives = 5;

    public int Lives
    {
        get { return lives; }
        set 
        { 
            if (value <= 5) lives = value;
            livesBar.Refresh();
        }
    }
    private LivesBar livesBar;

    private int score = 0;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreBar.Refresh();
        }
    }
    private ScoreBar scoreBar;

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;

    private bool isGrounded = false;

    private Bullet bullet;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private Rigidbody2D rigidBody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        scoreBar = FindObjectOfType<ScoreBar>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if(isGrounded) State = CharState.Idle;

        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump") && isGrounded) Jump();
    }

    private void Run()
    {
        if (Time.timeScale != 0.0F)
        {
            Vector3 direction = transform.right * Input.GetAxis("Horizontal");

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

            sprite.flipX = direction.x > 0.0F;

            if (isGrounded) State = CharState.Run;
        }
    }

    private void Jump()
    {
        rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        if (Time.timeScale != 0.0F)
        {
            Vector3 position = transform.position;
            position.y += 0.5F;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);

            newBullet.Parent = gameObject;
            newBullet.Direction = newBullet.transform.right * (sprite.flipX ? 1.0F : -1.0F);
        }
    }

    private int _prevTimeStamp = 0;

    private void Delay()
    {
        _prevTimeStamp = 0;
    }

    public override void ReceiveDamage()
    {
        if (_prevTimeStamp == 0)
        {
            Lives--;

            int direction;

            if (sprite.flipX) direction = -1;
            else direction = 1;

            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(transform.up * 6.0F + transform.right * direction * 6.0F, ForceMode2D.Impulse);

            Debug.Log(lives);

            _prevTimeStamp = 1;

            Invoke("Delay", 0.1F);
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if(!isGrounded) State = CharState.Jump;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Unit unit = collider.gameObject.GetComponent<Unit>();       
        //if (unit) ReceiveDamage();
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump
}
