using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy3 : MonoBehaviour
{
    protected bool isGrounded;
    protected bool isStunned;
    protected bool isDeath;
    protected bool isReadyToJump;
    protected int level;
    protected float jumpForce;
    protected float speed;
    EnemyGenerator3 enemyGenerator;
    MultiEnemyGeneratorController multiEnemyGenerator;
    Animator anim;
    Collider2D col2D;
    Rigidbody2D rb2d;
    protected Transform transformEnemy;
    protected SpriteRenderer sprite;
    public enum Direction { Left, Right };
    protected Direction direction;

    void Awake()
    {
        level = 1;

        transformEnemy = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        enemyGenerator = FindObjectOfType<EnemyGenerator3>();
        multiEnemyGenerator =
            FindObjectOfType<MultiEnemyGeneratorController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        jumpForce = 3.4f;
        speed = 2f;
        isStunned = false;
        isDeath = false;
        isReadyToJump = false;
        if (transformEnemy.position.x < 0)
            direction = Direction.Right;
        else
            direction = Direction.Left;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (isGrounded)
        {
            anim.SetBool("Ground", true);
        }
        else
        {
            anim.SetBool("Ground", false);
        }
    }

    void FixedUpdate()
    {
        if (isStunned == false && isReadyToJump)
        {
            if (direction == Direction.Right)
            {
                rb2d.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                rb2d.AddForce(new Vector2(-speed, jumpForce), ForceMode2D.Impulse);
            }
            isReadyToJump = false;
        }
        else if (isDeath)
        {
            transformEnemy.position = new Vector2(
                transformEnemy.position.x, transformEnemy.position.y - 0.07f);
            col2D.enabled = false;
        }

        if ((transformEnemy.position.x < -8.8
                || transformEnemy.position.x > 8.8))
        {
            if (transformEnemy.position.y < -2.5)
            {
                if (multiEnemyGenerator == null)
                    enemyGenerator.CreateEnemy(this.level, this.speed, 3);
                else
                    multiEnemyGenerator.CreateEnemy(this.level, this.speed, 3);
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
            if (SceneManager.GetActiveScene().name.StartsWith("Scene"))
                GameSceneController.EnemyKilled(200);
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
        else if ((trigger.tag == "Enemy" && isStunned == false) ||
                    (trigger.tag == "Player" && isStunned == false) ||
                        (trigger.tag == "FireBall" && isStunned == false))
        {
            if (direction == Direction.Right)
            {
                direction = Direction.Left;
            }
            else
            {
                direction = Direction.Right;
            }
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
        speed += speed / 5;
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
        }
    }

    public bool GetStun()
    {
        return isStunned;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void ReadyToJump()
    {
        this.isReadyToJump = true;
    }

    public void Revive()
    {
        this.isStunned = false;
        LevelUp();
    }
}
