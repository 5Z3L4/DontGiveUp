using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    Transform spawnposition;

    public int maxHealth = 1;
    public int healthPoints = 1;
    private Animator anim;
    public bool isDead = false;
    public static int souls;

    private void Awake()
    {
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
        if(isDead)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                anim.Play("PlayerResurection");
                isDead = false;
                healthPoints = maxHealth;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spieks"))
        {
            healthPoints = 0;
            transform.position = spawnposition.position;
        }
    }

    public void AddHealth()
    {
        maxHealth++;
        healthPoints = maxHealth;
    }

}
