/***************************************************************
* Copyright (C): 2015  LuWei
* Author       : LuWei
* Version      : V1.01
* Create       : 2017/2/27 14:56:15
* Note:        手势
***************************************************************/
using UnityEngine;
using System.Collections;

public class InputControl : MonoBehaviour
{
    [HideInInspector]
    public float moveX;
    [HideInInspector]
    public float moveY;
    [HideInInspector]
    public float lookX;
    [HideInInspector]
    public float lookY;

    /// <summary>
    ///   
    /// </summary>
    public bool IsMove;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private Vector2 last_hover_position;
//    private int valid_fingerId = -1;



    void Start()
    {
       
#if UNITY_IPHONE || UNITY_ANDROID
        //允许多点触屏
        Input.multiTouchEnabled = true; 
#endif
    }

    //
    void Update()
    {
        lookX = 0f;
        lookY = 0f;
        IsMove = false;
#if UNITY_IPHONE || UNITY_ANDROID
        if (Input.touchCount > 0) 
        {
            for (int i = 0; i < Input.touchCount; i++) 
            {
                UICamera.MouseOrTouch touch = UICamera.GetTouch(i);
                if (touch != null) 
                {
                    if (touch.pressed != null
                            && touch.current == touch.pressed
                            && (touch.current.GetComponent<BoxCollider2D>()
                                || touch.current.GetComponent<BoxCollider>()))
                    {
                        continue;
                    }
                }

                if (ClickTouch(i))         //点击触摸
                {
                    contorller_.Fire(Input.touches[i].position);
                }
                if (EndedTouch(i))         //结束触摸
                {
                    if (valid_fingerId == Input.touches[i].fingerId) 
                    {
                        valid_fingerId = -1;
                        last_hover_position.x = -1;
                        last_hover_position.y = -1;
                    }
                }
               else if (MoveSingleTouch(i))     //移动触摸
               {
                    if (valid_fingerId == -1)
                        valid_fingerId = Input.touches[i].fingerId;
                    if (valid_fingerId != -1 && valid_fingerId != Input.touches[i].fingerId) 
                     continue;

                    if (last_hover_position.x > -1 && last_hover_position.y > -1)
                    {
                        Vector2 delta_move = (Vector2)Input.touches[i].position - last_hover_position;
                        delta_move.x /= Screen.width;
                        delta_move.y /= Screen.width;

                        lookInput = delta_move;
                        lookX = delta_move.x + AccelerateInput(lookInput.x);
                        lookY = delta_move.y + AccelerateInput(lookInput.y);
                    }
                    contorller_.Fire(Input.touches[i].position);
                    last_hover_position = Input.touches[i].position;
                }

            }
        }
#else
        if (Input.GetButton("Fire1"))
        {
            IsMove = true;
            if (last_hover_position.x > -1 && last_hover_position.y > -1)
            {
                Vector2 delta_move = (Vector2)Input.mousePosition - last_hover_position;
                delta_move.x /= Screen.width;
                delta_move.y /= Screen.width;

                lookInput = delta_move;
                lookX = delta_move.x + AccelerateInput(lookInput.x);
                lookY = delta_move.y + AccelerateInput(lookInput.y);
            }

            last_hover_position = Input.mousePosition;
        }
        else
        {
            last_hover_position.x = -1;
            last_hover_position.y = -1;
        }
#endif
    }

    /// <summary>
    ///   
    /// </summary>
    float AccelerateInput(float input)
    {
        float inputAccel;
        inputAccel = ((1.0f / 2.0f) * input * (Mathf.Abs(input) * 4.0f)) * Time.smoothDeltaTime * 60.0f;
        return inputAccel;
    }

    /// <summary>
    ///   单点触摸
    /// </summary>
    bool SingleTouch()
    {
        if (Input.touchCount == 1)
            return true;
        return false;
    }

    /// <summary>
    ///   点击触摸
    /// </summary>
    bool ClickTouch(int idx)
    {
        if (Input.GetTouch(idx).phase == TouchPhase.Began)
            return true;
        return false;
    }

    /// <summary>
    ///   移动触摸
    /// </summary>
    bool MoveSingleTouch(int idx)
    {
        if (Input.GetTouch(idx).phase == TouchPhase.Moved)
            return true;
        return false;
    }

    /// <summary>
    ///   多点触摸
    /// </summary>
    bool MultipointTouch()
    {
        if (Input.touchCount > 1)
            return true;
        return false;
    }

    /// <summary>
    ///   开始触摸
    /// </summary>
    bool StartTouch(int idx)
    {
        if (Input.GetTouch(idx).phase == TouchPhase.Began)
            return true;
        return false;
    }

    /// <summary>
    ///   结束触摸
    /// </summary>
    bool EndedTouch(int idx)
    {
        if (Input.GetTouch(idx).phase == TouchPhase.Ended)
            return true;
        return false;
    }
}
