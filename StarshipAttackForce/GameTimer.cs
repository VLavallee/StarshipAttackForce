using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float gameTimer;
    void Start()
    {
        gameTimer = 0;
    }
    
    void Update()
    {
        gameTimer += Time.deltaTime;
    }
}
