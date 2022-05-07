using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private float speed = 5f;
	float shootTimer = 0.2f;

	private Rigidbody rb;
	private AudioSource shootSound;

	public GameObject obstacleDetector;
	public GameObject cameraPivot;
	private float obstacleDetectorZPos;
	public GameObject player;
	
	private float cameraStartYPos;
	private Vector3 cameraStartLocalPos;
	private PlayerShootManager psm;


	public bool rightAreaRotateCamera = false;

	void Start () {
		Application.targetFrameRate = 300;
		//shootSound = GameObject.Find("ShotSound").GetComponent<AudioSource> ();
		psm = gameObject.GetComponent<PlayerShootManager>();
		rb = GetComponent<Rigidbody> ();
		obstacleDetectorZPos = obstacleDetector.transform.localPosition.z;
		cameraStartYPos = Camera.main.transform.localPosition.y;
		cameraStartLocalPos = Camera.main.transform.localPosition;
	}
	
	void FixedUpdate () {
		Vector3 moveX = JoystickLeft.positionX * speed * transform.right;
		Vector3 moveY = JoystickLeft.positionY * speed * transform.forward;
		if(rightAreaRotateCamera) {
			if(JoystickLeft.positionX != 0) {
				float distance = Vector2.Distance(new Vector2(JoystickLeft.positionX, 0), new Vector2(0, JoystickLeft.positionY));
				if(JoystickRight.shot && JoystickLeft.positionY < 0) {
					rb.MovePosition(rb.position + (transform.forward * -distance + moveX * Time.fixedDeltaTime) / 10);
				}else {
					rb.MovePosition(rb.position + (transform.forward * distance + moveX * Time.fixedDeltaTime) / 10);
				}
			}
		}else {
			rb.MovePosition(transform.position + moveX * Time.fixedDeltaTime + moveY  * Time.fixedDeltaTime);
		}
		
	}

	void Update() {
		shootTimer += Time.deltaTime;
		if(JoystickRight.shot) {
			if(shootTimer >= 0.2f) {
				shootTimer = 0;
				//shootSound.Play();
				ShootBullets();
				
			}
			transform.rotation = Quaternion.Euler(0, JoystickRight.rotX, 0);
		}
		if(rightAreaRotateCamera) {
			if(JoystickLeft.angle != 0 && !JoystickRight.shot)
				transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * JoystickLeft.angle  + 180 + cameraPivot.transform.eulerAngles.y, Vector3.up);
		}else {
			transform.rotation = Quaternion.Euler(0, JoystickRight.rotX, 0);
		}
		cameraPivot.transform.rotation = Quaternion.Euler(JoystickRight.rotY, JoystickRight.rotX, 0);

		

		RaycastHit rayToCameraPos;
		if(Physics.Linecast (player.transform.position, obstacleDetector.transform.position, out rayToCameraPos)) {
			float distance = Vector3.Distance(new Vector3(rayToCameraPos.point.x, obstacleDetector.transform.position.y,rayToCameraPos.point.z), obstacleDetector.transform.position);
			if(obstacleDetector.transform.localPosition.z >= obstacleDetectorZPos) {
				obstacleDetector.transform.localPosition = Vector3.Lerp(obstacleDetector.transform.localPosition,
				new Vector3(obstacleDetector.transform.localPosition.x, obstacleDetector.transform.localPosition.y, obstacleDetector.transform.localPosition.z + (distance - 0.01f)), 100 * Time.deltaTime);
				
				float calculateCameraNewYPos = (cameraStartYPos / obstacleDetectorZPos) * obstacleDetector.transform.localPosition.z;
				if(calculateCameraNewYPos > 1f) {
					Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, calculateCameraNewYPos, Camera.main.transform.localPosition.z);
				}else {
					Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, 1f, Camera.main.transform.localPosition.z);
				}
			}else {
				obstacleDetector.transform.localPosition = new Vector3(obstacleDetector.transform.localPosition.x, obstacleDetector.transform.localPosition.y, obstacleDetectorZPos);
			}
		}else {
			if(!Physics.Linecast (player.transform.position, obstacleDetector.transform.position)) {
				if(obstacleDetector.transform.localPosition.z > obstacleDetectorZPos) {
					obstacleDetector.transform.localPosition = Vector3.Lerp(obstacleDetector.transform.localPosition,
					new Vector3(obstacleDetector.transform.localPosition.x, obstacleDetector.transform.localPosition.y, obstacleDetectorZPos), 3 * Time.deltaTime);
					Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition , cameraStartLocalPos, 3 * Time.deltaTime);
				}
			}
		}
	}

	private void ShootBullets() {
		psm.PlayerShoot();
	}

	public void Jump() {
		if(JoystickRight.jump) {
			JoystickRight.jump = false;
			GetComponent<Rigidbody> ().AddForce(new Vector3(0, 300, 0));	
		}
	}

	void OnTriggerEnter(Collider col) {
		JoystickRight.jump = true;
	}
}
