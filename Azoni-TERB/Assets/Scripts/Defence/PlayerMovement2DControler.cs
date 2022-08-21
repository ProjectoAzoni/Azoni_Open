using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2DControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left")){
            gameObject.transform.Translate(-50f * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey("right")){
            gameObject.transform.Translate(50f * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey("down")){
            gameObject.transform.Translate(0 ,-50f * Time.deltaTime,0);
        }
        if(Input.GetKey("up")){
            gameObject.transform.Translate(0 ,50f * Time.deltaTime, 0);
        }
    }
   /*void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + JoystickLeft2.positionX / 10, transform.position.y + JoystickLeft2.positionY / 10, transform.position.z);
        
    }*/
}
