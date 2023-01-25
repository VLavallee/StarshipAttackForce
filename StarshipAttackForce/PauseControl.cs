using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public bool gameWasJustUnpaused = false;
    [SerializeField] float time;
    [SerializeField] float timeLimit = 0.5f;
    void Update()
    {
        if (!FindObjectOfType<StageManager>().isPaused && gameWasJustUnpaused)
        {
            time += Time.deltaTime;
            if (time > timeLimit)
            {
                RegainMovementControl();
                gameWasJustUnpaused = false;
                time = 0;
            }
        }
    }

    private void RegainMovementControl()
    {
        FindObjectOfType<PlayerMovement>().isPaused = false;
    }
}
