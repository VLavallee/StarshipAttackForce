using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoMovement : MonoBehaviour
{

    [SerializeField] bool isMovingLeft, isMovingRight, isCurrentlyDodgingPlayer;
    [SerializeField] float moveSpeed, dodgeSpeed, dodgeMin, dodgeMax;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float countedTime, timeLimit, notDodgingTimeLimitMin, notDodgingTimeLimitMax, notDodgingTimeLimit;
    [SerializeField] int currentSide;
    [SerializeField] EnemyVariables enemyVariables;
    void Start()
    {
        var randomRoll = Random.Range(0, 100);
        if(randomRoll >= 0 && randomRoll <= 50)
        {
            isMovingLeft = true;
            isCurrentlyDodgingPlayer = true;
        }
        else if (randomRoll >= 51 && randomRoll <= 100)
        {
            isMovingRight = true;
            isCurrentlyDodgingPlayer = true;
        }
        rb = GetComponent<Rigidbody2D>();
        enemyVariables = FindObjectOfType<EnemyVariables>();
        DodgePlayer();
    }
    
    void Update()
    {
        moveSpeed = enemyVariables.GetEnemyMoveSpeed();
        if(isCurrentlyDodgingPlayer)
        {
            countedTime += Time.deltaTime;
            if(countedTime > timeLimit)
            {
                StopSideMovement();
                isCurrentlyDodgingPlayer = false;
                isMovingLeft = false;
                isMovingRight = false;
                countedTime = 0;
                var randRoll = Random.Range(notDodgingTimeLimitMin, notDodgingTimeLimitMax);
                notDodgingTimeLimit = randRoll;
            }
        }
        if(!isCurrentlyDodgingPlayer)
        {
            notDodgingTimeLimit -= Time.deltaTime;
            if(notDodgingTimeLimit <= 0)
            {
                DodgePlayer();
            }
        }
        
    }

    private void FixedUpdate()
    {
        MoveEnemy();
        if(isCurrentlyDodgingPlayer)
        {
            if (isMovingLeft)
            {
                MoveLeft();
            }
            else if (isMovingRight)
            {
                MoveRight();
            }
        }
    }

    private void MoveEnemy()
    {
        rb.velocity = new Vector2(rb.velocity.x, -moveSpeed * Time.deltaTime);
    }
    private void MoveLeft()
    {
        rb.velocity = new Vector2(rb.velocity.x + -dodgeSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(rb.velocity.x + dodgeSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void StopSideMovement()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Force Right"))
        {
            MoveRight();
        }
        if (collision.gameObject.CompareTag("Force Left"))
        {
            MoveLeft();
        }
    }

    private void DodgePlayer()
    {
        if (isCurrentlyDodgingPlayer) { return; }
        
        var dodgeRoll = Random.Range(0, 99);
        var newDodgeSpeed = Random.Range(dodgeMin, dodgeMax);
        dodgeSpeed = newDodgeSpeed;
        if (dodgeRoll <= 50)
        {
            isMovingLeft = true;
            isMovingRight = false;
        }
        else if (dodgeRoll >= 51)
        {
            isMovingRight = true;
            isMovingLeft = false;
        }

        isCurrentlyDodgingPlayer = true;
    }
}
