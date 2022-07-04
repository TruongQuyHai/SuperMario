using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Animator animator;

    private float speed = 10f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(DisableBullet(3f));
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void MoveBullet()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }

    IEnumerator DisableBullet(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool isEnemy = collision.gameObject.tag == "Enemy";
        if (isEnemy) { 
            animator.Play("BulletExplode");
            StartCoroutine(DisableBullet(0.1f));
        }
    }
}
