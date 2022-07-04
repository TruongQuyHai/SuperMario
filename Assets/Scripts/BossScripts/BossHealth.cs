using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int health = 1;
    private Animator animator;
    private bool canDamage = true;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canDamage) return;
        if (collision.gameObject.tag == "Bullet")
        {
            canDamage = false;
            health--;
            if (health <= 0)
            {
                gameObject.GetComponent<BossAttack>().Deactivate();
                animator.Play("Die");
                StartCoroutine(Die());
            }
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(0.5f);
        canDamage = true;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
