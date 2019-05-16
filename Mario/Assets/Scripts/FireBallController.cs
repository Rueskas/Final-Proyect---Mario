using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    protected Transform transformFireBall;
    protected Animator anim;
    protected SpriteRenderer bodySprite;

    public CapsuleCollider2D[] colliders;

    protected bool movementActived;
    protected float speedX, speedY;
    protected int timer;

    // Start is called before the first frame update
    void Start()
    {
        transformFireBall = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        bodySprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        speedX = 0.025f;
        speedY = 0.025f;
        movementActived = false;
        timer = 5000;
    }
    
    void Update()
    {
        if (timer > 0)
            timer--;
        else
            Active();
    }

    void FixedUpdate()
    {
        if(movementActived)
        {
            transform.position = new Vector2(transform.position.x+speedX,
                transform.position.y + speedY);
        }

        if (transformFireBall.position.x < -9 || transformFireBall.position.x > 9)
        {
            transformFireBall.position = new Vector2(-transform.position.x, transform.position.y);
        }
    }

    public void Active()
    {
        bodySprite.enabled = true;
        anim.enabled = true;
    }

    public void StartMovement()
    {
        movementActived = true;
    }

    void CollideX()
    {
        speedX = -speedX;
    }

    void CollideY()
    {
        speedY = -speedY;
    }
}
