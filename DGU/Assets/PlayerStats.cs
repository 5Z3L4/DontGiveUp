using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 1;
    public int healthPoints = 1;
    private Animator anim;
    public bool isDead = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (healthPoints <=0 && !isDead)
        {
            isDead = true;
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
}
