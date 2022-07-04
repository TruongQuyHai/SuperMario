using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private float maxY, minY;
    private Animator animator;
    private Rigidbody2D rb;
    BoxCollider2D boxCollider;
    private float speed;

    private Vector3 moveDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
        moveDirection = Vector2.down;
        transform.position = new Vector3(transform.position.x, Random.Range(minY, maxY));
        StartCoroutine(ChangeMovement());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 move = moveDirection * Time.smoothDeltaTime * speed;
        Vector3 pos = transform.position + move;
        if (pos.y <= minY || pos.y >= maxY)
        {
            move.y = -move.y;
            moveDirection = pos.y < minY ? Vector3.up : Vector3.down;
        }
        transform.Translate(move);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        moveDirection = moveDirection == Vector3.up ? Vector3.down : Vector3.up;
        StartCoroutine(ChangeMovement());
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
