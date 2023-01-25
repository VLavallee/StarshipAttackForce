using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    [SerializeField] float xPosMax, xPosMin, yPosMax, yPosMin;
    [SerializeField] float moveSpeed, stopTimeLimit, stopTime;
    [SerializeField] Vector2 objectPosition;
    //[SerializeField] Transform thisObject;
    public bool isMovable;
    [SerializeField] bool canMoveRight, canMoveLeft, canMoveUp, canMoveDown;
    [SerializeField] bool isMovingRight, isMovingLeft, isMovingUp, isMovingDown;
    [SerializeField] bool isLooping, stopBeforeContinuing, isStopped;
    [SerializeField] List<string> movementOrder;
    [SerializeField] int currentMove;
    [SerializeField] int loopBackAfter, loopBackTo;
    
    StageManager stageManager;
    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
        currentMove = 0;
        loopBackAfter = movementOrder.Count;
    }

    private void Update()
    {
        if(currentMove >= loopBackAfter && isLooping)
        {
            currentMove = loopBackTo;
        }
        objectPosition.x = GetXPos();
        objectPosition.y = GetYPos();
        if (isMovable)
        {
            if (movementOrder[currentMove] == "right")
            {
                if(!isMovingRight && objectPosition.x != xPosMax)
                {
                    isMovingRight = true;
                }
                if (isMovingRight)
                {
                    MoveRight();
                    if (objectPosition.x == xPosMax)
                    {
                        if(!stopBeforeContinuing)
                        {
                            isMovingRight = false;
                            currentMove++;
                        }
                        if(stopBeforeContinuing)
                        {
                            isMovingRight = false;
                            isStopped = true;
                        }
                    }
                }
                if (isStopped)
                {
                    stopTime += Time.deltaTime;
                    if (stopTime > stopTimeLimit)
                    {
                        currentMove++;
                        stopTime = 0;
                        isStopped = false;
                    }
                }
            }
            if (movementOrder[currentMove] == "left")
            {
                if(!isMovingLeft && objectPosition.x != xPosMin)
                {
                    isMovingLeft = true;
                }
                if(isMovingLeft)
                {
                    MoveLeft();
                    if(objectPosition.x == xPosMin)
                    {
                        if (!stopBeforeContinuing)
                        {
                            isMovingLeft = false;
                            currentMove++;
                        }
                        if (stopBeforeContinuing)
                        {
                            isMovingLeft = false;
                            isStopped = true;
                        }
                    }
                }
                if (isStopped)
                {
                    stopTime += Time.deltaTime;
                    if (stopTime > stopTimeLimit)
                    {
                        currentMove++;
                        stopTime = 0;
                        isStopped = false;
                    }
                }
            }
            if (movementOrder[currentMove] == "up")
            {
                if(!isMovingUp && objectPosition.y != yPosMax)
                {
                    isMovingUp = true;
                }
                if(isMovingUp)
                {
                    MoveUp();
                    if(objectPosition.y == yPosMax)
                    {
                        if (!stopBeforeContinuing)
                        {
                            isMovingUp = false;
                            currentMove++;
                        }
                        if (stopBeforeContinuing)
                        {
                            isMovingUp = false;
                            isStopped = true;
                        }
                    }
                }
                if (isStopped)
                {
                    stopTime += Time.deltaTime;
                    if (stopTime > stopTimeLimit)
                    {
                        currentMove++;
                        stopTime = 0;
                        isStopped = false;
                    }
                }
            }
            if (movementOrder[currentMove] == "down")
            {
                if(!isMovingDown && objectPosition.y != yPosMin)
                {
                    isMovingDown = true;
                }
                if(isMovingDown)
                {
                    MoveDown();
                    if(objectPosition.y == yPosMin)
                    {
                        if (!stopBeforeContinuing)
                        {
                            isMovingDown = false;
                            currentMove++;
                        }
                        if (stopBeforeContinuing)
                        {
                            isMovingDown = false;
                            isStopped = true;
                        }
                    }
                }
                if (isStopped)
                {
                    stopTime += Time.deltaTime;
                    if (stopTime > stopTimeLimit)
                    {
                        currentMove++;
                        stopTime = 0;
                        isStopped = false;
                    }
                }
            }
        }
    }

    private float GetYPos()
    {
        return transform.localPosition.y;
    }
    private float GetXPos()
    {
        return transform.localPosition.x;
    }
    private void MoveUp()
    {
        objectPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        objectPosition.y = Mathf.Clamp(GetYPos(), yPosMin, yPosMax);
        transform.localPosition = objectPosition;
    }
    private void MoveDown()
    {
        objectPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        objectPosition.y = Mathf.Clamp(GetYPos(), yPosMin, yPosMax);
        transform.localPosition = objectPosition;
    }
    private void MoveRight()
    {
        objectPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        objectPosition.x = Mathf.Clamp(GetXPos(), xPosMin, xPosMax);
        transform.localPosition = objectPosition;
    }
    private void MoveLeft()
    {
        objectPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        objectPosition.x = Mathf.Clamp(GetXPos(), xPosMin, xPosMax);
        transform.localPosition = objectPosition;
    }
    public void ChangeMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }

}
