using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateDown : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    void Update()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
    }
}
