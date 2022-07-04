using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    void Start()
    {
        Invoke("DisableRock", 4f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamge();
            gameObject.SetActive(false);
        }
    }

    void DisableRock()
    {
        gameObject.SetActive(false);
    }
}
