using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement2Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        transform.position  = new Vector3(transform.position.x + JoystickLeft2.positionX / 10, transform.position.y, transform.position.z + JoystickLeft2.positionY / 10);
		transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickLeft2.angle + 180, Vector3.up);
    }
}
