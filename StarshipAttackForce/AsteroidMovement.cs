using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float countedTime, timeLimit;

    [SerializeField] bool isBackgroundAsteroid;
    [SerializeField] bool isForegroundAsteroid;

    [SerializeField] bool isPowerup;

    [SerializeField] EnemyVariables enemyVariables;

    private Vector2 screenBounds;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyVariables = FindObjectOfType<EnemyVariables>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if(!isBackgroundAsteroid && !isForegroundAsteroid && !isBackgroundAsteroid && !isPowerup)
        {
            moveSpeed = enemyVariables.GetAsteroidMoveSpeed();
        }
        else if (isBackgroundAsteroid && !isForegroundAsteroid)
        {
            moveSpeed = enemyVariables.GetAsteroidMoveSpeed() / 2;
        }
        else if(isForegroundAsteroid && !isBackgroundAsteroid)
        {
            moveSpeed = enemyVariables.GetAsteroidMoveSpeed() * 2;
        }
        else if (isPowerup)
        {
            moveSpeed = enemyVariables.GetAsteroidMoveSpeed() / 1.5f;
        }
        if(transform.position.y < -screenBounds.y * 2)
        {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate()
    {
        MoveAsteroid();
    }

    private void MoveAsteroid()
    {
        rb.velocity = new Vector2(rb.velocity.x, -moveSpeed * Time.deltaTime);
    }

    
}
