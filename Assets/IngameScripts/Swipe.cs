using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;
    public GameObject target;
    float speed = 200f;
    public BoxCollider cube;


    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
        if (transform.rotation != target.transform.rotation)
        {
            var step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
        }
    }

    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            if (fingerDown.y - fingerUp.y > 0 && fingerDown.x - fingerUp.x < 0)//left up swipe
            {
                OnLeftSwipeUp();
            }
            if (fingerDown.y - fingerUp.y > 0 && fingerDown.x - fingerUp.x > 0)//right up swipe
            {
                OnRightSwipeUp();
            }
            if (fingerDown.y - fingerUp.y < 0 && fingerDown.x - fingerUp.x < 0)//left down swipe
            {
                OnLeftSwipeDown();
            }
            if (fingerDown.y - fingerUp.y < 0 && fingerDown.x - fingerUp.x > 0)//right down swipe
            {
                OnRightSwipeDown();
            }
            fingerUp = fingerDown;
        }

        //Check if Horizontal swipe
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {

        
    }

    void OnSwipeDown()
    {
        
    }

    void OnSwipeLeft()
    {
        target.transform.Rotate(0, 90, 0, Space.World);
        Debug.Log("Swipe Left");
    }

    void OnSwipeRight()
    {
        target.transform.Rotate(0, -90, 0, Space.World);
        Debug.Log("Swipe Right");
    }
    void OnLeftSwipeUp()
    {
        target.transform.Rotate(90, 0, 0, Space.World);
        Debug.Log("Swipe Up Left");
    }
    void OnRightSwipeUp()
    {
        target.transform.Rotate(0, 0, -90, Space.World);
        Debug.Log("Swipe Up Right");
    }
    void OnLeftSwipeDown()
    {
        target.transform.Rotate(0, 0, 90, Space.World);
        Debug.Log("Swipe Down Left");
    }
    void OnRightSwipeDown()
    {
        target.transform.Rotate(-90, 0, 0, Space.World);
        Debug.Log("Swipe Down Right");
    }
}
