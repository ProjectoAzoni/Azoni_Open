using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement2Controller : MonoBehaviour
{
    [SerializeField] JoystickLeft jl;
    private float speed = 5f;
    public bool rightAreaRotateCamera = false;
    private Rigidbody rb;
    private float turnSmoothtime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody> ();   
    }
	void FixedUpdate () {
		Vector3 moveX = JoystickLeft.positionX * speed * transform.right;
		Vector3 moveY = JoystickLeft.positionY * speed * transform.forward;
		rb.MovePosition(transform.position + moveX * Time.fixedDeltaTime + moveY  * Time.fixedDeltaTime);
        //Transform stick = jl.stick.transform;
        //Vector3 pos = stick.transform.position;
   
        //transform.rotation = Quaternion.Euler(0f,Mathf.SmoothDampAngle(transform.eulerAngles.y,Mathf.Atan(JoystickLeft.positionY/JoystickLeft.positionX)*Mathf.Rad2Deg, ref turnSmoothtime,0.5f),0f);
        
	}
    // Update is called once per frame
    void Update()
    {
        
    }

}
