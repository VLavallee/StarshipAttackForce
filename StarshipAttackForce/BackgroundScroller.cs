using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed;
    Material myMaterial;
    Vector2 offset;

    
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, backgroundScrollSpeed);
    }

    
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
