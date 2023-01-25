using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCanvasOffSwitch : MonoBehaviour
{
    [SerializeField] GameObject scoreCanvas;

    private void Start()
    {
        scoreCanvas.SetActive(true);
    }
    public void TurnOffScoreCanvas()
    {
        scoreCanvas.SetActive(false);
    }
}
