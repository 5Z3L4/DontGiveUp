using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreen;
    private PlayerStats playerStats;
    public bool isDead;
    public float delayTime = 0.3f;
    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    void Update()
    {
        isDead = playerStats.isDead;
        if(isDead)
        {
            deathScreen.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            deathScreen.SetActive(false);
            Cursor.visible = false;
        }
    }
}
