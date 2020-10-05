using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyMaxHP = 3;
    public float enemyHP = 3;
    public float speed;
    private bool movingRight = true;
    public Transform groundDetection;
    private float currentDistance;
    private Transform target;
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    private float stopDistance =1;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRangeX;
    public float attackRangeY;
    bool isPlayerDead = false;
    public Animator anim;
    private Vector3 respawnPos;
    public Vector3 deathPoint;
    private PlayerStats playerStats;
    float currentTime;

    public bool shouldIRespawn = true;
    private bool isDead = false;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("MiddleOfThePlayer").GetComponent<Transform>();
        respawnPos = transform.position;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    // Update is called once per frame
    void Update()
    {
        if(playerStats.isDead)
        {
            Respawn();
        }
        Attack();
        Move();
        Die();
        currentDistance = Mathf.Abs(Mathf.Abs(target.transform.position.x) - Mathf.Abs(transform.position.x));
    }

    public void GetDamage(float damage)
    {
        enemyHP -= damage;
        SoundManager.PlaySound(SoundManager.Sound.EnemyHit, attackPos.transform.position);
        anim.Play("SnakeHit");
        if (enemyHP <= 0)
        {
            currentTime = Time.time;
            SoundManager.PlaySound(SoundManager.Sound.EnemyDie, attackPos.transform.position);
        }
    }

    public void Die()
    {
        if (enemyHP <=0)
        {
            if(!isDead)
            {
                PlayerStats.souls += 2;
                isDead = true;
            }
            float deathTimer = 0.3f;
            if (currentTime + deathTimer < Time.time)
            {
                transform.position = deathPoint;

            }

        }
    }
    public void Move()
    {
        if (currentDistance >= stopDistance || isPlayerDead)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
            if (groundInfo.collider == false)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
        
    }

    private void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
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
                        anim.Play("SnakeAttack");
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
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }

    private void Respawn()
    {
        if(shouldIRespawn && isDead)
        {
            isDead = false;
            transform.position = respawnPos;
            enemyHP = enemyMaxHP;
        } 
    }
}
