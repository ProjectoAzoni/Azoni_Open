using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement2d : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private Vector3 scale;
   
    private RaycastHit2D hit;
    public float moveSpeed;
    
    private GameObject player;

    private float x;
    private float y;

   
    [SerializeField] GameObject ControlCanvas;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        scale = player.transform.localScale;


 
    }

 
    void Update()
    {

   
        ////movement system
        ///
        Vector3 moveX = JoystickLeft.positionX* transform.right;
        Vector3 moveY = JoystickLeft.positionY * transform.up;

        x = moveX.x;
        y = moveY.y;

      


        ////movement system
        moveDelta = new Vector3(x, y, 0) * moveSpeed;
        if (x == -1 && scale.x >= 0)
        {
            scale.x = scale.x * -1;
            player.transform.localScale = scale;

        }

        else if (x == 1 && scale.x <= 0)
        {
            scale.x = scale.x * -1;
            player.transform.localScale = scale;

        }





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
 

    
}
