using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private GameObject rock;
    [SerializeField] private Transform rockPos;
    private Animator animator;

    private readonly string coroutineName = "Attack";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(coroutineName);
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamge();
        }
    }

    public void Deactivate()
    {
        StopCoroutine(coroutineName);
        enabled = false;
    }

    void ThrowRock()
    {
        GameObject rockObj = Instantiate(rock, rockPos.position, Quaternion.identity);
        rockObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-300f, -700f), 0));
    }

    void Idle()
    {
        animator.Play("Idle");
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        StartCoroutine(coroutineName);
        animator.Play(coroutineName);
    }
}
