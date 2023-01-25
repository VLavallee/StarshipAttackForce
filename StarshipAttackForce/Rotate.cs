using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed0, rotateSpeed1;

    [SerializeField] bool isReversingRotation = false;
    [SerializeField] float elapsedTime, elapsedTimeLimit;
    [SerializeField] int rotationDirection;
    public void ChangeRotateSpeed(float newRotateSpeed0, float newRotateSpeed1)
    {
        rotateSpeed0 = newRotateSpeed0;
        rotateSpeed1 = newRotateSpeed1;
    }
    void Update()
    {
        if(!isReversingRotation)
        {
            transform.Rotate(new Vector3(0f, 0f, rotateSpeed0 * Time.deltaTime));
        }
        if(isReversingRotation)
        {
            if(rotationDirection == 0)
            {
                transform.Rotate(new Vector3(0f, 0f, rotateSpeed0 * Time.deltaTime));
                elapsedTime += Time.deltaTime;
                if(elapsedTime > elapsedTimeLimit)
                {
                    rotationDirection = 1;
                    elapsedTime = 0;
                }
            }
            if(rotationDirection == 1)
            {
                transform.Rotate(new Vector3(0f, 0f, -rotateSpeed1 * Time.deltaTime));
                elapsedTime += Time.deltaTime;
                if (elapsedTime > elapsedTimeLimit)
                {
                    rotationDirection = 0;
                    elapsedTime = 0;
                }
            }
        }
    }
}
