using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float damage;
    private Animator anim;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerStats.isDead)
        {
            Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeBtwAttack = startTimeBtwAttack;
                anim.Play("PlayerAttack");
                SoundManager.PlaySound(SoundManager.Sound.PlayerAttack, attackPos.transform.position);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().GetDamage(damage);
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

}
