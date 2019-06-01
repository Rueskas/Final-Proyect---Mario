using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    protected Transform transformPlayer;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rb2d;
    protected Animator anim;
    protected Collider2D[] colliders2D;

    public GameObject objectLives;
    protected SpriteRenderer[] spritesLives;
    protected struct Coordinates
    {
        public float posX, posY;
        public Coordinates(float positionX, float positionY)
        {
            posX = positionX;
            posY = positionY;
        }
    }

    protected Coordinates coordinatesLives;

    protected float horizontalInput;
    protected float jumpInput;

    protected float speed;
    protected float maxSpeed;

    protected float jumpForce;
    protected bool isGrounded;
    protected bool isIdle;
    protected bool isDamaged;
    protected bool isFalling;
    protected bool isBurned;
    protected bool isEnemyStunned;
    protected bool alive;

    protected int lives;

    protected AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip reviveSound;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transformPlayer = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        colliders2D = GetComponentsInChildren<Collider2D>();
        spritesLives = objectLives.GetComponentsInChildren<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetPositionLives();
        
        maxSpeed = 4;
        speed = 5;
        jumpForce = 3.4f;
        lives = 3;

        alive = true;
    }

    private void Update()
    {
        if(horizontalInput > 0)
        {
            sprite.flipX = true;
            anim.SetBool("Idle", false);
        }
        else if(horizontalInput < 0)
        {
            sprite.flipX = false;
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
        }

        if(isGrounded)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }

        if (isDamaged && isBurned)
        {
            anim.SetBool("Damaged", true);
            anim.SetBool("Burned", true);
        }
        else if (isDamaged)
        {
            anim.SetBool("Damaged", true);
        }
        else
        {
            anim.SetBool("Damaged", false);
            anim.SetBool("Burned", false);
        }

        if(isFalling)
        {
            anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Falling", false);
        }

        if (alive)
        {
            anim.SetBool("Alive", true);
        }
        else
        {
            anim.SetBool("Alive", false);
        }

        objectLives.transform.position = new Vector2(coordinatesLives.posX,
                coordinatesLives.posY);
        
    }

    private void FixedUpdate()
    {
        GetInput();
        
        if(!isDamaged)
            rb2d.AddForce(Vector2.right * speed * horizontalInput);

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        else if(rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        if (jumpInput != 0 && isGrounded)
        {
            speed = 5;
            rb2d.velocity = new Vector2(0,rb2d.velocity.y);
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
        else
        {
            speed = 15;
        }
        if (transformPlayer.position.x < -9 || transformPlayer.position.x > 9)
        {
            transformPlayer.position = new Vector2(-transform.position.x, transform.position.y);
        }

        if(isFalling)
        {
            transform.position = new Vector2(
                transform.position.x, transform.position.y - 0.07f);
        }

        if(alive && transform.position.y < -5.5)
        {
            lives--;
            if(lives > 0)
            {
                alive = false;
                spritesLives[lives - 1].enabled = false;
                Revive();
            }
            else
            {
                GameSceneController.PlayerDeath();
                Destroy(gameObject);
            }
        }
        else if (!alive && transform.position.y > 3.0f)
        {
            if(transform.position.y > 3.3f)
            {
                transform.position = new Vector2(0, transform.position.y - 0.001f);
            }
            else if (Input.anyKey)
            {
                Restart();
            }
            else
            {
                transform.position = new Vector2(0, 3.2f);
            }
        }
        
    }

    protected abstract void GetInput();

    protected abstract void SetPositionLives();


    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.tag == "Enemy")
        {
            isEnemyStunned =
                trigger.GetComponentInChildren<Animator>().GetBool("Stun");
            if(!isEnemyStunned)
            {
                Damaged();
            }
        }

        else if (trigger.tag == "FireBall")
        {
            Burned();
        }
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.tag == "ground" || trigger.tag == "Pow")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.tag == "ground" || trigger.tag == "Pow")
        {
            isGrounded = false;
        }
    }

    private void Fall()
    {
        for (int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = false;
        }
        isFalling = true;
    }

    private void Revive()
    {
        audioSource.clip = reviveSound;
        audioSource.Play();
        transform.position = new Vector2(0, 5);
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    private void Restart()
    {
        for (int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = true;
        }
        isFalling = false;
        isDamaged = false;
        isBurned = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        alive = true;
    }

    public void Damaged()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
        isDamaged = true;
        rb2d.velocity = new Vector2(0, 0);
    }

    public void Burned()
    {
        isBurned = true;
        Damaged();
    }

    public int GetLives()
    {
        return lives;
    }
}
