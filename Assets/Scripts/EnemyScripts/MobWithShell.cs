using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobWithShell : Mob
{
    //private Rigidbody2D rb;
    //private BoxCollider2D boxCollider;
    //private Animator animator;

    //private float speed;
    //private bool moveLeft;
    //private bool canMove;
    private bool stunned;
    private bool pushToLeft;
    private bool pushToRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.5f;
        moveLeft = true;
        canMove = true;

        pushToLeft = pushToRight = stunned = false;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckEdgeOrDistance();
        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;
        if (collision.gameObject.tag == "Player")
        {
            if (!stunned)
            {
                if (normal.y == -1)
                {
                    collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x, 7f);
                    Hitted();
                }
                else if (Mathf.Abs(normal.x) == 1)
                {
                    collision.gameObject.GetComponent<PlayerDamage>().DealDamge();
                }
            }
            else if (stunned)
            {
                if (normal.x == 1)
                    pushToRight = true;
                else if (normal.x == -1)
                    pushToLeft = true;

                Hitted();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EdgeCheck>())
        {
            ChangeDirection();
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Hitted();
        }
    }

    void ChangeDirection()
    {
        Vector3 temp;

        moveLeft = !moveLeft;
        if (moveLeft)
        {
            temp = transform.localScale;
            temp.x = Mathf.Abs(temp.x);
        }
        else
        {
            temp = transform.localScale;
            temp.x = -Mathf.Abs(temp.x);
        }
        transform.localScale = temp;
    }

    void Move()
    {
        float pushSpeed = 15f;
        if (pushToLeft) rb.velocity = new Vector2(-pushSpeed, rb.velocity.y);
        if (pushToRight) rb.velocity = new Vector2(pushSpeed, rb.velocity.y);

        if (!canMove) return;

        if (moveLeft)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Stunned()
    {
        canMove = false;
        stunned = true;
        animator.Play("Stunned");
    }

    public override void Hitted()
    {
        if (!stunned) { Stunned(); return; }

        animator.Play("Die");
        StartCoroutine(MobDie());
    }

    IEnumerator MobDie()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
