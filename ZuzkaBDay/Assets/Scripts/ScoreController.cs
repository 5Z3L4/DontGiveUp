using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject papor;
    public AudioSource krem;
    public GameObject paporMode;
    public GameObject paporBg;
    public GameObject paporMusic;
    public GameObject notPaporMode;
    public GameObject hoeMode;
    public AudioSource choking;
    public GameObject hoeModeBg;
    public GameObject hoePlayer;
    public GameObject hoeMusic;
    public GameObject zuzka;
    public GameObject zuzkaMusic;
    public AudioSource drinking;


    private float score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + "‰";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bad")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (collision.tag == "Alco")
        {
            Destroy(collision.gameObject);
            if (score > 21 && score < 22 && paporMode.activeSelf == false && hoeMode.activeSelf == false)
            {
                score = 21.37f;
                papor.SetActive(true);
                paporMode.SetActive(true);
                notPaporMode.SetActive(false);
                paporBg.SetActive(true);
                zuzka.SetActive(false);
                zuzkaMusic.SetActive(false);
                paporMusic.SetActive(true);
                
}
            else if (score < 21)
            {
                score += 1.18f;
                drinking.Play();
            }
            else if (score > 21 && score < 69)
            {
                score += 2;
                krem.Play();
            }
            
            if (score > 69 && paporMode.activeSelf == true  && hoeMode.activeSelf == false)
            {
                score = 69;
                papor.SetActive(false);
                paporMode.SetActive(false);
                paporBg.SetActive(false);
                hoeMode.SetActive(true);
                hoePlayer.SetActive(true);
                hoeMusic.SetActive(true);
                hoeModeBg.SetActive(true);
                paporMusic.SetActive(false);
            }
            else if (score >= 69)
            {
                score += 6;
                choking.Play();
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
