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
    public bool isTouchActive;
    [SerializeField] GameObject ControlCanvas;
    TouchControlManager2d touchControlManager2d;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player");
        
        touchControlManager2d = ControlCanvas.GetComponent<TouchControlManager2d>();
    }

    // Update is called once per frame
    void Update()
    {
       // isTouchActive = touchControlManager2d.IsTouchActive();
        TouchControl();
        
       

        moveDelta = new Vector3(x, y, 0) * moveSpeed;
        //movement system
        /*
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
            transform.Translate(1, 0, 0);

        }else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
           
        }*/

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
        if (isTouchActive)
        {
            x = touchControlManager2d.x;
            y = touchControlManager2d.y;
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }
    }
}
