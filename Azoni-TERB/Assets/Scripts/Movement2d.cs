using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement2d : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public float moveSpeed;
    
    private GameObject player;

    private float x;
    private float y;
   
    [SerializeField] GameObject ControlCanvas;
    TouchControlManager2d touchControlManager2d;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        
        touchControlManager2d = ControlCanvas.GetComponent<TouchControlManager2d>();
    }

 
    void Update()
    {

        TouchControl();


        //movement system
        moveDelta = new Vector3(x, y, 0) * moveSpeed;
        
  

        //collision system
        hit = Physics2D.BoxCast(transform.position,boxCollider.size,0,new Vector2(0,moveDelta.y),Mathf.Abs(moveDelta.y*Time.deltaTime),LayerMask.GetMask("Actor","Block"));
        if (hit.collider == null)
        {
            player.transform.Translate(0, moveDelta.y * Time.deltaTime , 0);

        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Block"));
        if (hit.collider == null)
        {
            player.transform.Translate(moveDelta.x * Time.deltaTime, 0,0);
        }
    }
    void TouchControl()
    {
        x = touchControlManager2d.GetX();
        y = touchControlManager2d.GetY();
        //if (isTouchActive)
        //{

        //}
        //else
        //{
        //    x = Input.GetAxisRaw("Horizontal");
        //    y = Input.GetAxisRaw("Vertical");
        //}
    }
}
