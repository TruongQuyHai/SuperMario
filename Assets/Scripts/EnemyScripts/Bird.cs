using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [SerializeField] private float radius = 5f;
    [SerializeField] private GameObject egg;
    [SerializeField] private LayerMask playerLayer;

    private bool attacked;
    private float speed;
    private bool canMove;
    private bool moveLeft;

    private float minX;
    private float maxX;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        attacked = false;
        canMove = true;
        moveLeft = true;
        minX = transform.position.x - radius;
        maxX = transform.position.x + radius;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();
        Move();
        CheckAndDropEgg();
    }

    void Move()
    {
        if (!canMove) return;

        if (moveLeft)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void ChangeDirection()
    {
        Vector3 temp;
        float x = transform.position.x;

        if (x <= minX) moveLeft = false;
        if (x >= maxX) moveLeft = true;

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

    void CheckAndDropEgg()
    {
        if (attacked) return;

        RaycastHit2D raycast = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, Mathf.Infinity, playerLayer);
        if (raycast.collider != null && raycast.collider.gameObject.tag == "Player")
        {
            Instantiate(egg, new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z), Quaternion.identity);
            attacked = true;
            animator.Play("Flying");
        }
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
        if (collision.gameObject.tag == "Bullet")
        {
            animator.Play("Die");
            boxCollider.isTrigger = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Invoke("Die", 2f);
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
