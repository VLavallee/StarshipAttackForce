using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTester : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate;
    public void ButtonPress()
    {
        objectToActivate.SetActive(true);
        Debug.Log("Button Pressed!");
    }
}
