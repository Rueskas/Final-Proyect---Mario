using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayersController2 : MonoBehaviour
{
    Transform transformPlayer;
    SpriteRenderer sprite;
    Rigidbody2D rb2d;
    Animator anim;
    Collider2D[] colliders2D;

    float horizontalInput;
    float jumpInput;

    float speed;
    float jumpForce;

    bool isGrounded;
    bool isIdle;
    bool isDamaged;
    bool isFalling;
    bool isBurned;
    bool isEnemyStunned;

    int lives;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transformPlayer = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        colliders2D = GetComponentsInChildren<Collider2D>();
    }

    void Start()
    {
        speed = 0.1f;
        jumpForce = 3.4f;
    }

    void Update()
    {
        GetInput();
        if (horizontalInput != 0)
        {
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
        }

        if (isGrounded)
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
    }

    private void FixedUpdate()
    {

        GetInput();

        if (horizontalInput > 0)
        {
            transformPlayer.position = new Vector3(transformPlayer.position.x + speed, transformPlayer.position.y, 0);
            sprite.flipX = true;
        }
        else if (horizontalInput < 0)
        {
            transformPlayer.position = new Vector3(transformPlayer.position.x - speed, transformPlayer.position.y, 0);
            sprite.flipX = false;
            anim.SetBool("Idle", false);
        }

        if (jumpInput != 0 && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (transformPlayer.position.x < -9 || transformPlayer.position.x > 9)
        {
            transformPlayer.position = new Vector2(-transform.position.x, transform.position.y);
        }

        if (isFalling)
        {
            transform.position = new Vector2(
                transform.position.x, transform.position.y - 0.07f);
        }

        if (transform.position.y < -5.5)
        {
            lives--;
            if (lives > 0)
            {
                transform.position = new Vector2(0, -3.5f); // Provisional
            }
            else
            {
                //END GAME TO DO
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump2");
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Enemy")
        {
            isEnemyStunned =
                trigger.GetComponentInChildren<Animator>().GetBool("Stun");
            if (!isEnemyStunned)
            {
                isDamaged = true;
            }
        }

        else if (trigger.tag == "FireBall")
        {
            isDamaged = true;
            isBurned = true;
        }
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.tag == "ground")
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.tag == "ground")
        {
            isGrounded = false;
        }
    }

    public void Fall()
    {
        for (int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = false;
        }
        isFalling = true;
    }

    public int GetLives()
    {
        return lives;
    }
}
