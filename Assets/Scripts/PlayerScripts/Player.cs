using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float speed = 5f;
    private float jumpPower = 12f;

    private Rigidbody2D pRigidbody;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private Vector3 start;

    public void ResetPos()
    {
        transform.position = start;
    }

    private void Awake()
    {
        start = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        pRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckJump();
    }

    private void CheckJump()
    {
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        if (raycastHit.collider != null)
        {
            animator.SetBool("Jump", false);

            if (Input.GetKey(KeyCode.Space))
            {
                pRigidbody.velocity = new Vector2(pRigidbody.velocity.x, jumpPower);
                animator.SetBool("Jump", true);
            }
        }
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float direction = Math.Abs(transform.localScale.x);

        pRigidbody.velocity = new Vector2(h * speed, pRigidbody.velocity.y);
        if (h > 0)
        {
            ChangeDirection(direction);
        }
        else if (h < 0)
        {
            ChangeDirection(-direction);
        }
        else
        {
            pRigidbody.velocity = new Vector2(0f, pRigidbody.velocity.y);
        }

        animator.SetInteger("Speed", (int)pRigidbody.velocity.x);
    }

    private void ChangeDirection(float direction)
    {
        Vector3 temp = transform.localScale;
        temp.x = direction;
        transform.localScale = temp;
    }
}
