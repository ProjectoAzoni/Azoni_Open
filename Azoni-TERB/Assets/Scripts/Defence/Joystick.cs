using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading;

public class Joystick : MonoBehaviour
{
    public GameObject stick;
	private RectTransform stickRectTransform;
    [HideInInspector]
	public GameObject bgImage;
    [HideInInspector]
	public int sMT = 30;
    [HideInInspector]
	public float posX;
    [HideInInspector]
	public float posY;
    [HideInInspector]
	public float angle;
    [HideInInspector]
	public bool onDrag;

	public void Start () {
		if(Screen.width > Screen.height) {
			sMT = sMT * (Screen.width + Screen.height) / 100;
		}else {
			sMT = sMT * (Screen.height + Screen.width) / 100;
		}
		
		stickRectTransform = stick.GetComponent<RectTransform>();
	}

	public void Move(BaseEventData data, bool freezeX, bool freezeY) {
		PointerEventData pointerData = data as PointerEventData;

		float x = bgImage.transform.position.x - pointerData.position.x;
		float y = bgImage.transform.position.y - pointerData.position.y;

		angle = Mathf.Atan2(x, y);

		float joystickXPosition = x;
		float joystickYPosition = y;

		if(Vector2.Distance(bgImage.transform.position, pointerData.position) > sMT) {
			joystickXPosition = sMT * Mathf.Sin(angle);
			joystickYPosition = sMT * Mathf.Cos(angle);
		}

		posX = -joystickXPosition / sMT;
		posY = -joystickYPosition / sMT;

		if(freezeX) {
			posX = 0;
			joystickXPosition = 0;
			joystickYPosition = y;
			if(joystickYPosition > sMT) {
				joystickYPosition = sMT;
			}
			if(joystickYPosition < -sMT) {
				joystickYPosition = -sMT;
			}
		}

		if(freezeY) {
			posY = 0;
			joystickYPosition = 0;
			if(!freezeX) {
				joystickXPosition = x;
				if(joystickXPosition > sMT) {
					joystickXPosition = sMT;
				}
				if(joystickXPosition < -sMT) {
					joystickXPosition = -sMT;
				}
			}
		}

		stick.transform.position = new Vector2(bgImage.transform.position.x - joystickXPosition, bgImage.transform.position.y - joystickYPosition);
		x = bgImage.transform.position.x - stick.transform.position.x;
		y = bgImage.transform.position.y - stick.transform.position.y;
		angle = Mathf.Atan2(x, y);

		if(onDrag) {
			float joysticXkBaseMovement = bgImage.transform.position.x - (stick.transform.position.x - pointerData.position.x);
			float joystickYBaseMovement = bgImage.transform.position.y - (stick.transform.position.y - pointerData.position.y);
			if(freezeX) joysticXkBaseMovement = bgImage.transform.position.x;
			if(freezeY) joystickYBaseMovement = bgImage.transform.position.y;
			bgImage.transform.position = new Vector2(joysticXkBaseMovement, joystickYBaseMovement);
		}
	}

	public void ReturnToNormalPosition() {
		stickRectTransform.anchoredPosition = new Vector2(0,0);
		posX = 0;
		posY = 0;
	}
}
