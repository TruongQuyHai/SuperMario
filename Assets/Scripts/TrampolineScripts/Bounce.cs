using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum State
{
    Idle,
    Down,
    Up
}
public class Bounce : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private GameObject player;
    private Animator anim;
    private bool down;
    private BoxCollider2D boxCollider;
    private State state = State.Idle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckPressed();
    }

    void CheckPressed()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.up, extraHeight, playerLayer);
        if (state == State.Idle && raycast.collider != null)
        {
            state = State.Down;
            anim.SetBool("Down", true);
            StartCoroutine(Up());
        }
    }

    void WaitIdle()
    {
        Invoke("Idle", 0.1f);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 22f);
    }

    void Idle()
    {
        state = State.Idle;
        anim.SetBool("Up", false);
    }

    IEnumerator Up()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Down", false);
        anim.SetBool("Up", true);
        state = State.Up;
    }
}
