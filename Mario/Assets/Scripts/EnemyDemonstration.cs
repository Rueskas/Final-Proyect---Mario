using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDemonstration : MonoBehaviour
{
    protected float speed;
    bool isStunned;
    bool isDeath;
    Animator anim;
    Collider2D col2D;
    public enum Direction { Left, Right };
    protected Direction direction;
    protected Transform transformEnemy;
    protected SpriteRenderer sprite;

    void Awake()
    {
        transformEnemy = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.03f;

        isStunned = false;
        isDeath = false;
        if (transformEnemy.position.x < 0)
            direction = Direction.Right;
        else
            direction = Direction.Left;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Direction.Right)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }


        if (isDeath)
        {
            anim.SetBool("Death", true);
        }
        else
        {
            anim.SetBool("Death", false);
        }

        if (isStunned)
        {
            anim.SetBool("Stun", true);
        }
        else
        {
            anim.SetBool("Stun", false);
        }
    }

    void FixedUpdate()
    {
        if (isStunned == false)
        {
            if (direction == Direction.Right)
            {
                transformEnemy.position = new Vector2(
                    transformEnemy.position.x + speed,
                        transformEnemy.position.y);
            }
            else
            {
                transformEnemy.position = new Vector2(
                    transformEnemy.position.x - speed,
                        transformEnemy.position.y);
            }
        }
        else if (isDeath)
        {
            isStunned = false;
            transformEnemy.position = new Vector2(
                transformEnemy.position.x, transformEnemy.position.y - 0.07f);
            col2D.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Player" && isStunned)
        {
            isDeath = true;
        }
    }
    

    public void StunController()
    {
        if (isStunned == false)
        {
            isStunned = true;
        }
        else if ( isStunned)
        {
            isStunned = false;
        }
    }

    public bool IsStunned()
    {
        return isStunned;
    }
}
