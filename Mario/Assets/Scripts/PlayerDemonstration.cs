using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDemonstration : MonoBehaviour
{
    Transform transformPlayer;
    SpriteRenderer sprite;
    Rigidbody2D rb2d;
    Animator anim;
    Collider2D[] colliders2D;
    
    float speed;
    float jumpForce;

    bool isGrounded;

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
        speed = 0.05f;
        jumpForce = 3.4f;
    }

    private void Update()
    {

        if (isGrounded)
        {
            anim.SetBool("Grounded", true);
        }
        else
        {
            anim.SetBool("Grounded", false);
        }

    }

    public void MoveRight()
    {
        anim.SetBool("Idle", false);
        transformPlayer.position = new Vector3(transformPlayer.position.x + speed, transformPlayer.position.y, 0);
        sprite.flipX = true;
    }

    public void MoveLeft()
    {
        anim.SetBool("Idle", false);
        transformPlayer.position = new Vector3(transformPlayer.position.x - speed, transformPlayer.position.y, 0);
        sprite.flipX = false;
    }

    public void Jump()
    {
        rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
    
    public void SetIdle()
    {
        anim.SetBool("Idle", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.tag == "ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.tag == "ground")
        {
            isGrounded = false;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    
}
