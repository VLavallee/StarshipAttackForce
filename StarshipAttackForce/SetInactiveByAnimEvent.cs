using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveByAnimEvent : MonoBehaviour
{
    private void OnEnable()
    {
        //GetComponent<Animator>().Play("Game Saved Transition To Off");
    }
    public void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
