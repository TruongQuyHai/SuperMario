using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private bool canDamage = true;
    private int lifeCount = 3;
    private Text lifeCountText;

    void Start()
    {
        Time.timeScale = 1f;
        lifeCountText = GameObject.Find("Life Count Text").GetComponent<Text>();
        lifeCountText.text = "x" + lifeCount;
    }

    public void DealDamge()
    {
        if (canDamage)
        {
            lifeCount--;
            if (lifeCount == 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(restartGame());
                return;
            }

            lifeCountText.text = "x" + lifeCount;
            canDamage = false;
            StartCoroutine(resetCanDamage());
        }
    }

    IEnumerator resetCanDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator restartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Scene of Hai");
    }
}
