using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private Vector2 screenBounds;
    [SerializeField] float YPadding;
    [SerializeField] bool applyPadding = true;
    [SerializeField] bool isDisablingInsteadOfDestroying;
    private void Start()
    {
        if(applyPadding)
        {
            screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            transform.position = new Vector2(transform.position.x, screenBounds.y + YPadding);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDisablingInsteadOfDestroying)
        {
            collision.gameObject.SetActive(false);
            if(collision.gameObject.GetComponent<DisableParent>())
            {
                collision.gameObject.GetComponent<DisableParent>().DisableParentObj();
            }
            return;
        }
        Destroy(collision.gameObject);
    }
}
