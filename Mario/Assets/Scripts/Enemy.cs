using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float speed;
    bool isGrounded;
    bool isStunned;
    bool isDeath;
    bool isTurning;
    int level;
    Animator anim;
    Collider2D col2D;
    public enum Direction {Left, Right};
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
        speed = 0.05f;
        level = 1;
        
        isStunned = false;
        isDeath = false;
        isTurning = false;
        if (transformEnemy.position.x < 0)
            direction = Direction.Right;
        else
            direction = Direction.Left;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == Direction.Right)
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

        if(isTurning)
        {
            anim.SetBool("Turn", true);
        }
        else
        {
            anim.SetBool("Turn", false);
        }

        anim.SetInteger("Level", level); //For the generator TO DO
    }

    void FixedUpdate()
    {
        if (isStunned == false && isTurning == false)
        {
            if(direction == Direction.Right)
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
            transformEnemy.position = new Vector2(
                transformEnemy.position.x, transformEnemy.position.y-0.07f);
            col2D.enabled = false;
        }

        if ((transformEnemy.position.x < -8.8 
                || transformEnemy.position.x > 8.8)
                && isTurning == false)
        {
            if (transformEnemy.position.y < -2.5)
                Destroy(gameObject);
            else if (transformEnemy.position.x < 0)
                transformEnemy.position = new Vector2(
                    8.7f, transform.position.y);
            else
                transformEnemy.position = new Vector2(
                    -8.7f, transform.position.y);
        }
        else if (transformEnemy.position.y < -5.5)
            Destroy(gameObject);
        
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.tag == "ground")
        {
            isGrounded = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "PlayerHead" && isGrounded && isStunned == false)
        {
            isStunned = true;
        }
        else if (trigger.tag == "Player" && isGrounded && isStunned)
        {
            isDeath = true;
        }
        else if ((trigger.tag == "Enemy" && isTurning == false) || 
                    (trigger.tag == "Player" && isStunned == false) || 
                        (trigger.tag =="FireBall" && isTurning == false))
        {
            if(direction == Direction.Right)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Right;
            }
            isTurning = true;
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.tag == "ground")
        {
            isGrounded = false;
        }
    }

    public void LevelUp()
    {
        speed += speed/2;
        isStunned = false;
        level++;
    }

    public void EndTurn()
    {
        isTurning = false;
    }
    
    public void SetSpeed(int speed) //For the generator TO DO
    {
        this.speed = speed;
    }

    public void SetLevel(int level) //For the generator TO DO
    {
        this.level = level;
    }
}
