using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnemy : MonoBehaviour
{
    
    public bool facingRight = false;
    public float moveSpeed = 8;

    public float horizontal = 0;
    public float stopDistance = 0.5f;
    public float targetRange = 10f;
    private float currentDistance;
    private Rigidbody2D rb;
    private Transform target;
    private Animator anim;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float damage;
    private bool isPlayerDead = false;
    public float enemyHP = 4;

    bool isDamaged = false;
    float lastTimeDamaged = 0;
    private Vector3 respawnPos;
    private PlayerStats playerStats;
    private float enemyMaxHP = 4;
    private float currentTime;
    public Vector3 deathPoint;
    private bool isDead = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("MiddleOfThePlayer").GetComponent<Transform>();
        respawnPos = transform.position;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

    }

    private void Update()
    {
        FindTarget();
        Move(horizontal, rb);
        Attack();
        if (playerStats.isDead)
        {
            Respawn();
        }
        Die();
        currentDistance = Mathf.Abs(Mathf.Abs(target.transform.position.x) - Mathf.Abs(transform.position.x));
    }

    public void Move(float horizontal, Rigidbody2D rbx)
    {
        float damageDelay = 0.3f;
        if (lastTimeDamaged + damageDelay < Time.time)
        {
            isDamaged = false;
        }
        if(horizontal != 0 && !isDamaged)
        {
            anim.Play("DogEnemyWalk");
        }
        if (!isDamaged)
        {
            
            rbx.velocity = new Vector2(horizontal * moveSpeed * Time.deltaTime, rbx.velocity.y);
            if (horizontal > 0 && facingRight || horizontal < 0 && !facingRight)
            {
                Flip();
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    private void MoveTowardsPlayer()
    {
        if (target.transform.position.x > this.transform.position.x && currentDistance > stopDistance)
        {
            horizontal = 1;
        }
        else if (target.transform.position.x < this.transform.position.x && currentDistance > stopDistance)
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0;
        }
    }

    private void FindTarget()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < targetRange)
        {
            Debug.Log(Vector2.Distance(transform.position, target.transform.position));
            Debug.Log(targetRange);
            MoveTowardsPlayer();
        }
        else
        {
            horizontal = 0;
            anim.Play("DogEnemyIdle");
        }
    }

    private void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            isPlayerDead = enemiesToDamage[i].GetComponent<PlayerStats>().isDead;
        }

        if (timeBtwAttack <= 0)
        {

            if (currentDistance <= stopDistance && !isPlayerDead)
            {
                timeBtwAttack = startTimeBtwAttack;

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (!isPlayerDead)
                    {
                        enemiesToDamage[i].GetComponent<PlayerStats>().healthPoints -= 1;
                        Debug.Log(enemiesToDamage[i].GetComponent<PlayerStats>().healthPoints);
                        anim.Play("DogEnemyAttack");
                    }
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void GetDamage(float damage)
    {
        enemyHP -= damage;
        Debug.Log(enemyHP);
        
        SoundManager.PlaySound(SoundManager.Sound.EnemyHit, attackPos.transform.position);
        anim.Play("DogEnemyHit");
        isDamaged = true;
        lastTimeDamaged = Time.time;
        if (enemyHP <= 0)
        {
            currentTime = Time.time;
            SoundManager.PlaySound(SoundManager.Sound.EnemyDie, attackPos.transform.position);
        }

    }

    public void Die()
    {
        if (enemyHP <= 0)
        {
            isDead = true;
            float deathTimer = 0.3f;
            if (currentTime + deathTimer < Time.time)
            {
                transform.position = deathPoint;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spieks"))
        {
            GetDamage(enemyHP);
        }
    }

    private void Respawn()
    {
        if(isDead)
        {
            isDead = false;
            transform.position = respawnPos;
            enemyHP = enemyMaxHP;
        }
       
    }
}
