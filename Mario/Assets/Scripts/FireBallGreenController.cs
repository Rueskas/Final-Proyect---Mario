using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallGreenController : MonoBehaviour
{
    protected Transform transformFireBall;
    protected Animator anim;
    protected SpriteRenderer bodySprite;
    protected ColliderY collY;
    protected CircleCollider2D coll;

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
        collY = gameObject.GetComponentInChildren<ColliderY>();
        coll = GetComponent<CircleCollider2D>();

        speedX = 0.025f;
        speedY = 0.025f;
        movementActived = false;
        timer = 100;
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
        if (movementActived)
        {
            transform.position = new Vector2(transform.position.x + speedX,
                transform.position.y + speedY);
        }

        if (transformFireBall.position.x < -9 ||
                transformFireBall.position.x > 9)
        {
            transformFireBall.position = 
                new Vector2(-transform.position.x, transform.position.y);
        }

        if (transformFireBall.position.y < 0.3 ||
                transformFireBall.position.y > 0.8f)
        {
            CollideY();
        }
    }

    public void Active()
    {
        bodySprite.enabled = true;
        anim.enabled = true;
        collY.enabled = true;
        coll.enabled = true;
    }

    public void StartMovement()
    {
        movementActived = true;
    }

    void CollideY()
    {
        speedY = -speedY;
    }
}
