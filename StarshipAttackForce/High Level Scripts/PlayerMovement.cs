using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of ‘Player’s’ movement. Depending on the chosen handling type, it moves the ‘Player’ together with the pointer.
/// </summary>

//enumerating 'Player's' handling types
public enum HandlingType { pointerPosition, offset }



[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMovement : MonoBehaviour
{

    public HandlingType handlingType;

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    public bool isPaused = false;
    Camera mainCamera;
    Vector3 distanseToPointer; //distance to 'Player's' touch or mouse position when using handling type 'offset'
    public bool controlIsActive = true;
    private Vector3 pointerPosOffset; // amount is preset in the start method;

    public static PlayerMovement instance; //unique instance of the script for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        pointerPosOffset.y = 1.5f;
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders deending on Viewport's size
    }

    private void Update()
    {
        if (isPaused) { return; }
        if (controlIsActive)
        {
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL   //if the current platform is not mobile, setting mouse handling 
            FindAndSetHandlingType();
            if (Input.GetMouseButton(0)) //if mouse button was pressed       
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //calculating mouse position in the worldspace
                mousePosition.z = transform.position.z;
                if (handlingType == HandlingType.offset) //if handling type 'offset', moving the object considering distance to pointer, if not, moving the object to pointer position
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        distanseToPointer = mousePosition - transform.position;
                    }
                    transform.position = mousePosition - distanseToPointer;
                }
                else if (handlingType == HandlingType.pointerPosition)
                    transform.position = Vector3.MoveTowards(transform.position, mousePosition + pointerPosOffset, 30 * Time.deltaTime);
            }
#endif

#if UNITY_IOS || UNITY_ANDROID //if current platform is mobile, 

            if (Input.touchCount == 1) // if there is a touch
            {
                FindAndSetHandlingType();
                Touch touch = Input.touches[0];
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);  //calculating touch position in the world space
                touchPosition.z = transform.position.z;
                if (handlingType == HandlingType.offset) //if handling type 'offset', moving the object considering distance to pointer, if not, moving the object to pointer position
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        distanseToPointer = touchPosition - transform.position;
                    }
                    transform.position = touchPosition - distanseToPointer;
                }
                else if (handlingType == HandlingType.pointerPosition)
                    transform.position = Vector3.MoveTowards(transform.position, touchPosition + pointerPosOffset, 30 * Time.deltaTime);
            }
#endif
            transform.position = new Vector3    //if 'Player' crossed the movement borders, returning him back 
                (
                Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
                0
                );
        }
    }

    //setting 'Player's' movement borders according to Viewport size and defined offset
    void ResizeBorders()
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }

    public void ChangePlayerControl(string trueOrFalse)
    {
        if (trueOrFalse == "true")
        {
            controlIsActive = true;
        }
        if (trueOrFalse == "false")
        {
            controlIsActive = false;
        }
    }
    public void FindAndSetHandlingType()
    {
        if (FindObjectOfType<GameSession>().GetControlType() == 1)
        {
            handlingType = HandlingType.offset;
            return;
        }
        if (FindObjectOfType<GameSession>().GetControlType() == 2)
        {
            handlingType = HandlingType.pointerPosition;
            return;
        }
        Debug.Log(handlingType);
    }
}
