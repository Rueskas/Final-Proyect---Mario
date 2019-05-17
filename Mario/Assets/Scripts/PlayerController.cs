﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    private void Start()
    {
        speed = 0.1f;
        jumpForce = 3.4f;
        lives = 3;
    }

    private void Update()
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
        
    }

    private void FixedUpdate()
    {
        GetInput();

        if (horizontalInput > 0 && !isDamaged)
        {
            transformPlayer.position = new Vector3(transformPlayer.position.x + speed, transformPlayer.position.y, 0);
            sprite.flipX = true;
        }
        else if (horizontalInput < 0 && !isDamaged)
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

        if(isFalling)
        {
            transform.position = new Vector2(
                transform.position.x, transform.position.y - 0.07f);
        }

        if(transform.position.y < -5.5)
        {
            lives--;
            if(lives > 0)
            {
                Restart();// Provisional
            }
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");
    }

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
                isDamaged = true;
            }
        }

        else if (trigger.tag == "FireBall")
        {
            isDamaged = true;
            isBurned = true;
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

    private void Restart()
    {
        for (int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = true;
        }
        transform.position = new Vector2(0, -3.5f); 
        isFalling = false;
        isDamaged = false;
        isBurned = false;
    }

    public int GetLives()
    {
        return lives;
    }
}
