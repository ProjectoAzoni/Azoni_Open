using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JoystickRight2 : MonoBehaviour
{
    public GameObject stick;
	public GameObject bgImage;
	public GameObject rightArea;
	[Range(1, 10)]
	public int sMT = 4;
	public bool sticky = false;
	public bool onDrag = false;
    [HideInInspector]
	public bool freezeX = false;
    [HideInInspector]
	public bool freezeY = false;
	public static float posX;
	public static float posY;
	public static float angle;

	Joystick joystick;

	void Start() {
        joystick = gameObject.GetComponent<Joystick>();
		Init();
	}

	public void Init () {
		joystick.stick = stick;
		joystick.bgImage = bgImage;
		joystick.sMT = sMT;
		joystick.onDrag = onDrag;
		joystick.Start();
		
		if(sticky) {
			bgImage.SetActive(false);
			stick.SetActive(false);
			rightArea.SetActive(true);
		}else {
			bgImage.SetActive(true);
			stick.SetActive(true);
			rightArea.SetActive(false);
		}
	}
	
	public void Move(BaseEventData data) {
		joystick.Move(data, freezeX, freezeY);
		posX = joystick.posX;
		posY = joystick.posY;
		angle = joystick.angle;
	}

	public void ReturnToNormalPosition() {
		joystick.ReturnToNormalPosition();
		posX = joystick.posX;
		posY = joystick.posY;
	}

	//Methods bellow are used if sticky joystick option is enabled
	public void OnStickyPointerDown(BaseEventData data) {
		PointerEventData pointerData = data as PointerEventData;
		bgImage.SetActive(true);
		stick.SetActive(true);
		bgImage.transform.position = pointerData.position;
	}

	public void OnStickyPointerUp(BaseEventData data) {
		joystick.ReturnToNormalPosition();
		posX = joystick.posX;
		posY = joystick.posY;
		bgImage.SetActive(false);
		stick.SetActive(false);
	}
}
