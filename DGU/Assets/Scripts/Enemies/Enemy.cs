using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float enemyMaxHP = 3;
    [SerializeField]
    private float enemyHP = 3;
    [SerializeField]
    private int enemyDamage = 1;
    [SerializeField]
    private int soulCount;

    [Header("Movement")]
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float stopDistance = 1;
    [SerializeField]
    protected bool movingRight = true;

    [Header("Attack")]
    [SerializeField]
    private float timeBtwAttack;
    [SerializeField]
    private float startTimeBtwAttack;
    [SerializeField]
    protected LayerMask whatIsEnemies;

    [Header("Spawn")]
    [SerializeField]
    private Vector3 deathPoint;
    [SerializeField]
    private bool shouldIRespawn = true;

    [Header("Others")]
    [SerializeField]
    protected string animName;
    public Transform attackPos;
    public Animator anim;

    protected float currentDistance; // current distance between enemy and player
    protected bool isPlayerDead = false;
    protected bool isDead = false;
    private float currentTime;
    private Transform target; // player
    private PlayerStats playerStats;
    private Vector3 respawnPos;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("MiddleOfThePlayer").GetComponent<Transform>();
        respawnPos = transform.position;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        deathPoint = GameObject.FindGameObjectWithTag("DeadBox").GetComponent<Transform>().transform.position;
    }

    public void GetDamage(float damage, string deathAnimName)
    {
        enemyHP -= damage;
        SoundManager.PlaySound(SoundManager.Sound.EnemyHit, attackPos.transform.position);
        anim.Play(deathAnimName);
        if (enemyHP <= 0)
        {
            currentTime = Time.time;
            SoundManager.PlaySound(SoundManager.Sound.EnemyDie, attackPos.transform.position);
        }
    }

    protected void Die()
    {
        if (enemyHP <= 0)
        {
            if (!isDead)
            {
                PlayerStats.souls += soulCount;
                isDead = true;
            }
            float deathTimer = 0.3f;
            if (currentTime + deathTimer < Time.time)
            {
                transform.position = deathPoint;
            }

        }
        if (playerStats.isDead)
        {
            Respawn(shouldIRespawn);
        }
    }

    public virtual void Attack(Collider2D[] enemiesToDamage, string animName)
    {
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
                        enemiesToDamage[i].GetComponent<PlayerStats>().healthPoints -= enemyDamage;
                        Debug.Log(enemiesToDamage[i].GetComponent<PlayerStats>().healthPoints);
                        anim.Play(animName);
                    }
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    protected void Respawn(bool shouldIRespawn)
    {
        if (shouldIRespawn && isDead)
        {
            isDead = false;
            transform.position = respawnPos;
            enemyHP = enemyMaxHP;
        }
    }

    protected void CheckDistance()
    {
        currentDistance = Mathf.Abs(Mathf.Abs(target.transform.position.x) - Mathf.Abs(transform.position.x));
    }
}
