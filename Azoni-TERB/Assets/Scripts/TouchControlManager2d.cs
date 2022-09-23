using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TouchControlManager2d : MonoBehaviour
{
    public  bool isActionButtonPressed;
    public  bool isTouchActive;
    [SerializeField] GameObject Canvas;
    public int x;
    public int y;
    

    void Start()
    {
        x = 0;
        y = 0;

        Canvas.SetActive(true);
    }
    /*void Update()
    {
        if (isTouchActive)
        {
            Canvas.SetActive(true);
        }
        else
        {
           Canvas.SetActive(false);
        }
    }*/

    public  bool IsTouchActive() { 

        return isTouchActive;
    }
    //VERTICAL MOVEMENT
        //UP BUTTON
            public void OnPointerDownUpButton()
            {
                y = 1;

            }
  
        //DOWN BUTTON
            public void OnPointerDownDownButton()
            {
                y = -1;

            }
    //POINTER UP
    public void OnPointerUpVertical()
    {
        y = 0;
    }

    //HORIZONTAL MOVEMENT
    //LEFT BUTTON
    public void OnPointerDownLeftButton()
        {
            x = -1;

        }

        //RIGHT BUTTON
        public void OnPointerDownRightButton()
        {
            x= 1;

        }
        //POINTER UP
        public void OnPointerUpHorizontal()
        {
            x = 0;
        }
    

    //ACTION BUTTON
    public void OnPointerDown()
    {
        isActionButtonPressed= true;
       
    }
    public void OnPointerUp()
    {
        isActionButtonPressed = false;
        
    }
    public bool GetIsActionButtonPressed()
    {
        return isActionButtonPressed;
    }
}
