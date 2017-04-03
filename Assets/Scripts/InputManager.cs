using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 0.5f;

    public static bool isTouch = false;

    public GameManager gameManager;
    public CTouchAni touchAni;
    public IntroManager introManager;


    // Update is called once per frame
    void Update()
    {
        if (GameManager.GAMEOVER) return;
        if (Input.GetMouseButtonDown(0))
        {
            isSwipe = true;
            fingerStartTime = Time.time;
            fingerStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            float gestureTime = Time.time - fingerStartTime;
            float gestureDist = ((Vector2)Input.mousePosition - fingerStartPos).magnitude;

            if (isSwipe && gestureDist > minSwipeDist && gestureTime < maxSwipeTime)
            {
                Vector2 direction = (Vector2)Input.mousePosition - fingerStartPos;
                Vector2 swipeType = Vector2.zero;

                swipeType = Vector2.right * Mathf.Sign(direction.x) + Vector2.up * Mathf.Sign(direction.y);

                //Left Down Check
                if (swipeType.x != 0.0f)
                {
                    if (swipeType.x > 0.0f)
                    {
                        Debug.Log("Right");
                        // MOVE RIGHT
                    }
                    else
                    {
                        Debug.Log("Left");
                        // MOVE LEFT
                    }
                }

                if (swipeType.y != 0.0f)
                {
                    if (swipeType.y > 0.0f)
                    {
                        Debug.Log("Up");
                        // MOVE UP
                    }
                    else
                    {
                        Debug.Log("Down");
                        // MOVE DOWN
                        if (introManager != null)
                            introManager.swiped();


                    }
                }
            }
        }

         if (Input.GetMouseButtonUp(0))
         {
            Debug.Log("Mouse Up");

            if (introManager != null)
                introManager.up();

            if(isTouch)
            {
                isTouch = false;
                touchAni.TouchAnim();
                if(gameManager != null)
                    gameManager.LevelUp();
            
                // 이펙트
            }

            /*
            //float gestureTime = Time.time - fingerStartTime;
            float gestureDist = ((Vector2)Input.mousePosition - fingerStartPos).magnitude;

            //if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)

            if (isSwipe && gestureDist > minSwipeDist)
            {
                Vector2 direction = (Vector2)Input.mousePosition - fingerStartPos;
                Vector2 swipeType = Vector2.zero;

                swipeType = Vector2.right * Mathf.Sign(direction.x) + Vector2.up * Mathf.Sign(direction.y);

                //Left Down Check
                if(swipeType.x < 0.0f && swipeType.y < 0.0f)
                {
                    Debug.Log("Left/Down");
                }

                /*

                if (swipeType.x != 0.0f)
                {
                    if (swipeType.x > 0.0f)
                    {
                        Debug.Log("Right");                        
                        // MOVE RIGHT
                    }
                    else
                    {
                        Debug.Log("Left");
                        // MOVE LEFT
                    }
                }

                if (swipeType.y != 0.0f)
                {
                    if (swipeType.y > 0.0f)
                    {
                        Debug.Log("Up");
                        // MOVE UP
                    }
                    else
                    {
                        Debug.Log("Down");
                        // MOVE DOWN
                    }
                }
                */

            }
        }
    }
