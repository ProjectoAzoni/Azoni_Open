using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement2d : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    public int moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDelta = new Vector3(x, y, 0);
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;

        }else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.Translate(moveDelta * Time.deltaTime*moveSpeed);
    }
}
