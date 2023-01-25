using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingAstralObject : MonoBehaviour
{
    [SerializeField] float minScrollingSpeed, maxScrollingSpeed, scrollingSpeed;
    [SerializeField] Transform startPosition;
    [SerializeField] float minStartPositionX, maxStartPositionX;
    [SerializeField] float minStartPositionY, maxStartPositionY;
    [SerializeField] float minScaleRange;
    [SerializeField] float maxScaleRange;
    [SerializeField] float minWait, maxWait, timeSpentWaiting, timeLimit;

    [SerializeField] bool waitTimeActive;
    [SerializeField] bool fixedSpeed;
    [SerializeField] bool fixedSprites;
    [SerializeField] bool randomizeSizeAndPositionOnStart;


    [SerializeField] SpriteRenderer planetRenderer;
    [SerializeField] SpriteRenderer ringRenderer;

    [SerializeField] List<Sprite> planets;
    [SerializeField] List<Sprite> rings;

    [SerializeField] GameObject planetObj;
    [SerializeField] GameObject ringObj;

    [SerializeField] List<Transform> rotations;

    private void Start()
    {
        if(randomizeSizeAndPositionOnStart)
        {
            var newXPosition = Random.Range(minStartPositionX, maxStartPositionX);
            var newYPosition = Random.Range(minStartPositionY, maxStartPositionY);
            transform.position = new Vector2(newXPosition, newYPosition);
            var newScale = Random.Range(minScaleRange, maxScaleRange);
            transform.localScale = new Vector3(newScale, newScale, 1);
        }
    }

    void Update()
    {
        if(!waitTimeActive)
        {
            transform.Translate(0, -scrollingSpeed * Time.deltaTime, 0, Space.World);
        }
        else if(waitTimeActive)
        {
            timeSpentWaiting += Time.deltaTime;
            if(timeSpentWaiting >= timeLimit)
            {
                waitTimeActive = false;
            }
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision!");
        if(collision.gameObject.CompareTag("End"))
        {
            var newXPosition = Random.Range(minStartPositionX, maxStartPositionX);
            transform.position = new Vector2(newXPosition, startPosition.position.y);
            var newScale = Random.Range(minScaleRange, maxScaleRange);
            transform.localScale = new Vector3(newScale, newScale, 1);

            if(!fixedSpeed)
            {
                scrollingSpeed = Random.Range(minScrollingSpeed, maxScrollingSpeed);
            }

            timeLimit = Random.Range(minWait, maxWait);
            timeSpentWaiting = 0;
            waitTimeActive = true;

            if(!fixedSprites)
            {
                var planetRoll = Random.Range(0, planets.Count - 1);
                var ringRoll = Random.Range(0, rings.Count - 1);
                planetRenderer.sprite = planets[planetRoll];
                ringRenderer.sprite = rings[ringRoll];
            }
            

            var rotationRoll1 = Random.Range(0, rotations.Count - 1);
            var rotationRoll2 = Random.Range(0, rotations.Count - 1);
            if(planetObj != null)
            {
                planetObj.transform.rotation = Quaternion.identity;
                planetObj.transform.rotation = rotations[rotationRoll1].transform.rotation;
            }
            if(ringObj != null)
            {
                ringObj.transform.rotation = rotations[0].transform.rotation;
            }
            
            //ringObj.transform.rotation = rotations[rotationRoll2].transform.rotation;

            //Debug.Log(gameObject.name + " has rolled " + rotationRoll1);

        }
    }
}
