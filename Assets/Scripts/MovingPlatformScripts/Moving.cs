using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    private bool moveRight = true;
    private Vector3 moveDirection = Vector3.right;

    void Start()
    {
        StartCoroutine(MovingRoutine());
    }

    IEnumerator MovingRoutine()
    {
        yield return new WaitForSeconds(time);
        moveRight = !moveRight;
        moveDirection = moveRight ? Vector3.right : Vector3.left;
        StartCoroutine(MovingRoutine());
    }

    void Update()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }
}
