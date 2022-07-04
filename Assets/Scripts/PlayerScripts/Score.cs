using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private AudioSource coinAudio;
    private Text coinText;
    private int score;

    void Awake()
    {
        coinAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        coinText = GameObject.Find("Coins Count Text").GetComponent<Text>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            IncreaseScore();
            collision.gameObject.SetActive(false);
        }
    }

    public void IncreaseScore()
    {
        score++;
        coinAudio.Play();
        coinText.text = "x" + score;
    }
}
