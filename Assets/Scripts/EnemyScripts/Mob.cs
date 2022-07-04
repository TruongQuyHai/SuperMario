using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    protected Animator animator;

    protected float speed;
    protected bool moveLeft;
    protected bool canMove = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        //CheckEdgeOrDistance();
        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamge();
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
        if (!canMove) return;

        if (moveLeft)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public virtual void Hitted()
    {
        canMove = false;
        boxCollider.isTrigger = true;
        animator.Play("Die");
        StartCoroutine(MobDie());
    }

    IEnumerator MobDie()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
