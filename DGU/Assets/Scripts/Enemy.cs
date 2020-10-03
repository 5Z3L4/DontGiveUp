using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float enemyHP = 3;
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

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("MiddleOfThePlayer").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
        Move();
        currentDistance = Mathf.Abs(Mathf.Abs(target.transform.position.x) - Mathf.Abs(transform.position.x));
    }

    public void GetDamage(float damage)
    {
        enemyHP -= damage;
        Debug.Log(enemyHP);
        Die();
    }

    public void Die()
    {
        if (enemyHP <=0)
        {
            Debug.Log("EnemyDied");
            Destroy(gameObject);
        }
    }
    public void Move()
    {
        if (currentDistance >= stopDistance)
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
        if (timeBtwAttack <= 0)
        {
            if (currentDistance <= stopDistance)
            {
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY),0, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<PlayerStats>().healthPoints -= 1;
                    Debug.Log(enemiesToDamage[i].GetComponent<PlayerStats>().healthPoints);
                }
                //anim.Play("PigAttack");
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
}
