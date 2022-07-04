using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScreenWhenTouch : MonoBehaviour
{
    private GameObject player;
    private GameObject cam;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Start()
    {
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            StartCoroutine(restartGame());
        }
    }

    IEnumerator restartGame()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
        player.GetComponent<Player>().ResetPos();
        cam.GetComponent<CameraFollow>().ResetPos();
    }
}
