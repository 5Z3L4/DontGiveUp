using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    Transform spawnposition;

    public int maxHealth = 1;
    private bool isDeadBySpikes = false;
    public int healthPoints = 1;
    private Animator anim;
    public bool isDead = false;
    public static int souls;

    private void Awake()
    {
        Object.DontDestroyOnLoad(gameObject);
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (healthPoints <=0 && !isDead)
        {
            isDead = true;
            SoundManager.PlaySound(SoundManager.Sound.PlayerDie);
            anim.Play("PlayerDeath");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spieks"))
        {
            isDeadBySpikes = true;
            healthPoints = 0;

        }
        if(collision.CompareTag("Resp"))
        {
            spawnposition = collision.transform;
            GameObject.FindGameObjectWithTag("Level1").SetActive(false);
            GameObject.FindGameObjectWithTag("Level2").SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Spieks"))
        {
            isDeadBySpikes = false;
        }
    }
    public void AddHealth()
    {
        if(PlayerStats.souls>0)
        {
            maxHealth++;
            healthPoints = maxHealth;
            PlayerStats.souls -= 1;
        }
        
    }

    public void Resurrect()
    {
        if(!isDeadBySpikes && PlayerStats.souls >0)
        {
            healthPoints = maxHealth;
            isDead = false;
            anim.Play("PlayerResurection");
            PlayerStats.souls -= 1;
        }
    }

    public void ResurrectAtSpawn()
    {
        healthPoints = maxHealth;
        transform.position = spawnposition.position;
        isDead = false;
        anim.Play("PlayerResurection");

    }

}
