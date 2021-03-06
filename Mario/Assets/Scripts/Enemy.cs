﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    protected float speed;
    protected float limitSpeed;
    bool isGrounded;
    bool isStunned;
    bool isDeath;
    bool isTurning;
    int level;
    EnemyGenerator1 enemyGenerator;
    MultiEnemyGeneratorController multiEnemyGenerator;
    Animator anim;
    Collider2D col2D;
    public enum Direction {Left, Right};
    protected Direction direction;
    protected Transform transformEnemy;
    protected SpriteRenderer sprite;

    void Awake()
    {
        limitSpeed = 0.11f;
        speed = 0.05f;
        level = 1;

        transformEnemy = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
        enemyGenerator = FindObjectOfType<EnemyGenerator1>();
        multiEnemyGenerator =
            FindObjectOfType<MultiEnemyGeneratorController>();
    }
    // Start is called before the first frame update
    void Start()
    {
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
            {
                if (multiEnemyGenerator == null)
                    enemyGenerator.CreateEnemy(this.level, this.speed, 1);
                else
                    multiEnemyGenerator.CreateEnemy(this.level, this.speed, 1);
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
            if(SceneManager.GetActiveScene().name.StartsWith("Scene"))
                GameSceneController.EnemyKilled(50);
            else
                GameSceneController.EnemyKilled(0);
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
        if (speed > limitSpeed)
            speed = limitSpeed;
        isStunned = false;
        level++;
    }

    public void StunController()
    {
        if (isGrounded && isStunned == false)
        {
            isStunned = true;
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
