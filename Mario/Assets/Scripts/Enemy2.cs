using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    protected float speed;
    bool isGrounded;
    bool isStunned;
    bool isDeath;
    bool isTurning;
    bool reduceLevel;
    int level;
    EnemyGenerator2 enemyGenerator;
    Animator anim;
    Collider2D[] col2D;
    public enum Direction { Left, Right };
    protected Direction direction;
    protected Transform transformEnemy;
    protected SpriteRenderer sprite;

    void Awake()
    {
        level = 2;
        speed = 0.05f;

        transformEnemy = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        col2D = GetComponents<Collider2D>();
        enemyGenerator = FindObjectOfType<EnemyGenerator2>();
    }
    // Start is called before the first frame update
    void Start()
    {

        isStunned = false;
        isDeath = false;
        isTurning = false;
        reduceLevel = false;
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

        if (isTurning)
        {
            anim.SetBool("Turn", true);
        }
        else
        {
            anim.SetBool("Turn", false);
        }

        if(reduceLevel)
        {
            anim.SetBool("Reduce", true);
            reduceLevel = false;
        }
        else
        {
            anim.SetBool("Reduce", false);
        }
        anim.SetInteger("Level", level);
    }

    void FixedUpdate()
    {
        if (isStunned == false && isTurning == false)
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
            transformEnemy.position = new Vector2(
                transformEnemy.position.x, transformEnemy.position.y - 0.07f);
            for (int i = 0; i < col2D.Length; i++)
            {
                col2D[i].enabled = false;
            }
        }

        if ((transformEnemy.position.x < -8.8
                || transformEnemy.position.x > 8.8)
                && isTurning == false)
        {
            if (transformEnemy.position.y < -2.5)
            {
                enemyGenerator.CreateEnemy(this.level, this.speed);
                Destroy(gameObject);
            }
            else if (transformEnemy.position.x < 0)
                transformEnemy.position = new Vector2(
                    8.7f, transform.position.y);
            else
                transformEnemy.position = new Vector2(
                    -8.7f, transform.position.y);
        }
        else if (transformEnemy.position.y < -5.5)
        {
            Destroy(gameObject);
            GameSceneController.EnemyKilled(100);
        }

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
        if (trigger.tag == "Player" && isGrounded && isStunned)
        {
            isDeath = true;
        }
        else if ((trigger.tag == "Enemy" && isTurning == false) ||
                    (trigger.tag == "Player" && isStunned == false) ||
                        (trigger.tag == "FireBall" && isTurning == false))
        {
            if (direction == Direction.Right)
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
    
    public void StunController()
    {
        if (isGrounded && isStunned == false)
        {
            if (level == 2)
            {
                reduceLevel = true;
                level--;
                speed += speed / 2;
            }
            else if(level == 1)
            {
                level--;
                isStunned = true;
            }
        }
        else if (isGrounded && isStunned)
        {
            isStunned = false;
            isTurning = false;
        }
    }

    public void EndTurn()
    {
        isTurning = false;
    }

    public void LevelUp()
    {
        isStunned = false;
        level = 2;
    }

    public bool GetStun()
    {
        return isStunned;
    }

    public bool GetTurn()
    {
        return isTurning;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }
}
