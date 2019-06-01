using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    protected Transform transformFireBall;
    protected Animator anim;
    protected SpriteRenderer bodySprite;
    public GameObject collX;
    public GameObject collY;
    protected Collider2D[] collidersX;
    protected Collider2D[] collidersY;
    protected Collider2D coll;

    protected bool movementActived;
    protected float speedX, speedY;
    protected int timer;

    public AudioClip rebound;
    protected AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        transformFireBall = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        bodySprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        collidersX = collX.GetComponents<Collider2D>();
        collidersY = collY.GetComponents<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        speedX = 0.025f;
        speedY = 0.025f;
        movementActived = false;
        timer = 400;
        audioSource.clip = rebound;
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
            CollideX();
        }
        if (transformFireBall.position.y > 5 )
        {
            CollideY();
        }
    }

    public void Active()
    {
        bodySprite.enabled = true;
        anim.enabled = true;
        for (int i = 0; i < collidersX.Length; i++)
        {
            collidersX[i].enabled = true;
            collidersY[i].enabled = true;
        }
        coll.enabled = true;
        collX.GetComponent<ColliderX>().enabled = true;
        collY.GetComponent<ColliderY>().enabled = true;
}

    public void StartMovement()
    {
        movementActived = true;
    }

    void CollideX()
    {
        speedX = -speedX;
        audioSource.Play();
    }

    void CollideY()
    {
        speedY = -speedY;
        audioSource.Play();
    }
}
