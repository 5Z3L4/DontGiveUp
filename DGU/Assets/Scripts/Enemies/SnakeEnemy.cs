using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : Enemy
{
    public Transform groundDetection;
    public float attackRangeX;
    public float attackRangeY;
    // Start is called before the first frame update
    void Start()
    {
        animName = "SnakeAttack";
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();   
        Attack(Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies), animName);
        if (!isDead)
        {
            Move();
        }
        Die();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
