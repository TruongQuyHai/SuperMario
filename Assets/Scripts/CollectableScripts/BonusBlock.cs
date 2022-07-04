using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    private Animator anim;
    private Vector3 originPos;
    private Vector3 animPos;
    private bool startAnim;
    private bool canAnim;
    private Vector3 moveDirection = Vector3.up;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        originPos = transform.position;
        animPos = transform.position;
        animPos.y += 0.15f;
        canAnim = true;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateUpDown();
    }

    private void AnimateUpDown()
    {
        if (startAnim) {
            transform.Translate(moveDirection * Time.smoothDeltaTime);

            if (transform.position.y >= animPos.y)
            {
                moveDirection = Vector3.down;
            } else if (transform.position.y <= originPos.y)
            {
                startAnim = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 normal = collision.GetContact(0).normal;
            if (normal.y == 1 && canAnim)
            {
                anim.Play("Idle");
                startAnim = true;
                canAnim = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().IncreaseScore();
            }
        }
    }
}
